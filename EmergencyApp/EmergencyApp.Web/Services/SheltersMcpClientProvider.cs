using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace EmergencyApp.Web.Services;

/// <summary>
/// Singleton hosted service that maintains a persistent MCP connection to the SheltersApi
/// and exposes its tools as AIFunctions for use in the chat pipeline.
/// </summary>
public sealed class SheltersMcpClientProvider : IHostedService, IAsyncDisposable
{
    private McpClient? _client;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<SheltersMcpClientProvider> _logger;

    public IReadOnlyList<AIFunction> Tools { get; private set; } = [];

    public SheltersMcpClientProvider(
        IHttpClientFactory httpClientFactory,
        ILoggerFactory loggerFactory,
        ILogger<SheltersMcpClientProvider> logger)
    {
        _httpClientFactory = httpClientFactory;
        _loggerFactory = loggerFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("shelters-mcp");
            var transport = new HttpClientTransport(
                new HttpClientTransportOptions { Endpoint = httpClient.BaseAddress! },
                httpClient,
                _loggerFactory,
                false);

            _client = await McpClient.CreateAsync(transport, loggerFactory: _loggerFactory, cancellationToken: cancellationToken);

            var tools = await _client.ListToolsAsync(cancellationToken: cancellationToken);
            Tools = tools.Cast<AIFunction>().ToList();

            _logger.LogInformation(
                "Connected to Shelters MCP server. {Count} tool(s) loaded: {Names}",
                Tools.Count, string.Join(", ", Tools.Select(t => t.Name)));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not connect to Shelters MCP server — shelter tools will be unavailable.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        if (_client is not null)
            await _client.DisposeAsync();
    }
}
