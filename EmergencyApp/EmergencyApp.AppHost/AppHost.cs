var builder = DistributedApplication.CreateBuilder(args);

var openai = builder.AddConnectionString("openai");

var MarkItDownEndpointName = "http";
var markitdown = builder.AddContainer("markitdown", "mcp/markitdown")
    .WithArgs("--http", "--host", "0.0.0.0", "--port", "3001")
    .WithHttpEndpoint(targetPort: 3001, name: MarkItDownEndpointName);

var webApp = builder.AddProject<Projects.EmergencyApp_Web>("aichatweb-app");
webApp
    .WithReference(openai)
    .WithExternalHttpEndpoints();
webApp
    .WithEnvironment("MARKITDOWN_MCP_URL", markitdown.GetEndpoint(MarkItDownEndpointName));

builder.Build().Run();
