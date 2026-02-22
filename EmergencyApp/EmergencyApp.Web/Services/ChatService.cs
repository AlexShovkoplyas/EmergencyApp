using System.ComponentModel;
using Microsoft.Extensions.AI;

namespace EmergencyApp.Web.Services;

public class ChatService(IChatClient chatClient, SemanticSearch search) : IDisposable
{
    private const string SystemPrompt = @"
        You are an assistant who answers questions about information you retrieve.
        Do not answer questions about anything else.
        Use only simple markdown to format your responses.

        Use the LoadDocuments tool to prepare for searches before answering any questions.

        Use the Search tool to find relevant information. When you do this, end your
        reply with citations in the special XML format:

        <citation filename='string'>exact quote here</citation>

        Always include the citation in your response if there are results.

        The quote must be max 5 words, taken word-for-word from the search result, and is the basis for why the citation is relevant.
        Don't refer to the presence of citations; just emit these tags right at the end, with no surrounding text.
        ";

    private readonly List<ChatMessage> _messages = [];
    private readonly ChatOptions _chatOptions = new();
    private int _statefulMessageCount;
    private CancellationTokenSource? _currentResponseCancellation;

    public IReadOnlyList<ChatMessage> Messages => _messages;
    public ChatMessage? CurrentResponseMessage { get; private set; }

    /// <summary>
    /// Raised when the chat tool methods need the UI to re-render (e.g. during document loading or search).
    /// Subscribers should call StateHasChanged on the appropriate component.
    /// </summary>
    public event Func<Task>? OnStateChanged;

    /// <summary>
    /// Raised after each streaming chunk is received, passing the in-progress response message.
    /// Subscribers should call ChatMessageItem.NotifyChanged with the provided message.
    /// </summary>
    public event Action<ChatMessage>? OnMessageStreamed;

    public void Initialize()
    {
        _statefulMessageCount = 0;
        _messages.Clear();
        _messages.Add(new(ChatRole.System, SystemPrompt));
        _chatOptions.Tools =
        [
            AIFunctionFactory.Create(LoadDocumentsAsync),
            AIFunctionFactory.Create(SearchAsync),
        ];
    }

    public async Task AddUserMessageAsync(ChatMessage userMessage)
    {
        CancelAnyCurrentResponse();

        _messages.Add(userMessage);

        var responseText = new TextContent("");
        CurrentResponseMessage = new ChatMessage(ChatRole.Assistant, [responseText]);
        _currentResponseCancellation = new();

        await foreach (var update in chatClient.GetStreamingResponseAsync(
            _messages.Skip(_statefulMessageCount), _chatOptions, _currentResponseCancellation.Token))
        {
            _messages.AddMessages(update, filter: c => c is not TextContent);
            responseText.Text += update.Text;
            _chatOptions.ConversationId = update.ConversationId;
            OnMessageStreamed?.Invoke(CurrentResponseMessage!);
        }

        _messages.Add(CurrentResponseMessage!);
        _statefulMessageCount = _chatOptions.ConversationId is not null ? _messages.Count : 0;
        CurrentResponseMessage = null;
    }

    public void CancelAnyCurrentResponse()
    {
        if (CurrentResponseMessage is not null)
            _messages.Add(CurrentResponseMessage);

        _currentResponseCancellation?.Cancel();
        CurrentResponseMessage = null;
    }

    public Task ResetAsync()
    {
        CancelAnyCurrentResponse();
        _chatOptions.ConversationId = null;
        Initialize();
        return Task.CompletedTask;
    }

    [Description("Loads the documents needed for performing searches. Must be completed before a search can be executed, but only needs to be completed once.")]
    private async Task LoadDocumentsAsync()
    {
        if (OnStateChanged is not null)
            await OnStateChanged();

        await search.LoadDocumentsAsync();
    }

    [Description("Searches for information using a phrase or keyword. Relies on documents already being loaded.")]
    private async Task<IEnumerable<string>> SearchAsync(
        [Description("The phrase to search for.")] string searchPhrase,
        [Description("If possible, specify the filename to search that file only. If not provided or empty, the search includes all files.")] string? filenameFilter = null)
    {
        if (OnStateChanged is not null)
            await OnStateChanged();

        var results = await search.SearchAsync(searchPhrase, filenameFilter, maxResults: 5);
        return results.Select(result => $"<result filename=\"{result.DocumentId}\">{result.Text}</result>");
    }

    public void Dispose() => _currentResponseCancellation?.Cancel();
}
