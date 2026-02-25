using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using System.Text.Json;

namespace EmergencyApp.Web.Services;

public class GeoSearchAgentFactory(IChatClient chatClient, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory) : IDisposable
{
    private McpClient? _mcpClient;

    public async Task<AIAgent> BuildAsync()
    {
        // 1. Create the MCP Client (SSE Transport for Web Service)
        var httpClient = httpClientFactory.CreateClient("geo-search-mcp");
        
        if (httpClient.BaseAddress == null)
        {
            httpClient.BaseAddress = new Uri("http://localhost:5000/sse");
        }

        var transport = new HttpClientTransport(
            new HttpClientTransportOptions { Endpoint = httpClient.BaseAddress! },
            httpClient,
            loggerFactory,
            false);

        _mcpClient = await McpClient.CreateAsync(transport, loggerFactory: loggerFactory);

        // 2. Retrieve the list of tools available on the GitHub server
        var mcpTools = await _mcpClient.ListToolsAsync().ConfigureAwait(false);

        // 3. Convert MCP Tools to AIFunctions
        // Assuming McpTool inherits from AIFunction/AITool as seen in other providers
        var agentTools = mcpTools.Cast<AITool>().ToList();

        // 4. Create the Agent using the constructor that accepts instructions and tools
        AIAgent agent = new ChatClientAgent(
            chatClient,
            "GeoBot",
            "Handles GitHub repository searches.",
            "You answer questions related to GitHub repositories only.",
            agentTools
        );

        return agent;
    }

    public void Dispose()
    {
        (_mcpClient as IDisposable)?.Dispose();
    }
}
