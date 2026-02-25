using EmergencyApp.Web.Services;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace EmergencyApp.Web.Agents;

public class EmergencyNotificationProcessor
{
    private readonly AIAgent _summarizerAgent;
    private readonly EmailSender _emailSender;
    private readonly UserSettingsService _userSettingsService;

    public EmergencyNotificationProcessor([FromKeyedServices(AgentNames.SummarizeAgent)] AIAgent summarizerAgent, EmailSender emailSender, UserSettingsService userSettingsService)
    {
        _summarizerAgent = summarizerAgent;
        _emailSender = emailSender;
        this._userSettingsService = userSettingsService;
    }

    public async Task ProcessNotificationAsync(List<ChatMessage> history, string type)
    {
        string summary = "No history available.";
        if (history != null && history.Count > 0)
        {
            // Create a summarization prompt
            // We clone the list to avoid modification issues during enumeration if the main thread is adding to it
            var messages = new List<ChatMessage>(history);
            messages.Add(new ChatMessage(ChatRole.User, "Summarize the dangerous situation in 2-3 sentences for emergency responders based on the chat history above."));

            try
            {
                var response = await _summarizerAgent.RunAsync(messages);
                summary = response.ToString();
            }
            catch (Exception ex)
            {
                summary = $"Error generating summary: {ex.Message}";
            }
        }

        var settings = await _userSettingsService.LoadCurrentAsync();
        var toEmail = settings.contactPerson.Email;

        await _emailSender.SendAsync(toEmail, "Emergency", summary);
    }
}
