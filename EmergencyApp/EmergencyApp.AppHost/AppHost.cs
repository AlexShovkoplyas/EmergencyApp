using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

var openai = builder.AddConnectionString("openai");

var MarkItDownEndpointName = "http";
var markitdown = builder.AddContainer("markitdown", "mcp/markitdown")
    .WithArgs("--http", "--host", "0.0.0.0", "--port", "3001")
    .WithHttpEndpoint(targetPort: 3001, name: MarkItDownEndpointName);

var postgres = builder.AddAzurePostgresFlexibleServer("postgres")
    .RunAsContainer(x=>x.WithPgAdmin());

var postgresDb = postgres.AddDatabase("EmergencyAppDb");

// Azure Communication Services — provisioned via Bicep.
// Role assignment grants the current principal (developer locally, managed identity in ACA)
// Contributor access so DefaultAzureCredential works without a connection string.
var acs = builder.AddBicepTemplate("communication-services", "Bicep/communication-services.bicep")
    .WithParameter(AzureBicepResource.KnownParameters.PrincipalId)
    .WithParameter(AzureBicepResource.KnownParameters.PrincipalType);

var webApp = builder.AddProject<Projects.EmergencyApp_Web>("aichatweb-app");
webApp
    .WithReference(openai)
    .WithReference(postgresDb)
    .WaitFor(postgresDb)
    .WithEnvironment("ACS_ENDPOINT", acs.GetOutput("endpoint"))
    .WithExternalHttpEndpoints();
webApp
    .WithEnvironment("MARKITDOWN_MCP_URL", markitdown.GetEndpoint(MarkItDownEndpointName));

builder.Build().Run();
