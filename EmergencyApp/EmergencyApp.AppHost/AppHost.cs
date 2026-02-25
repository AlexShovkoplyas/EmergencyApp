using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

// var openai = builder.AddConnectionString("openai");
var openai = builder.AddOpenAI("openai")
    .WithEndpoint("https://models.inference.ai.azure.com");

var chat = openai.AddModel("chat", "gpt-4o-mini").WithHealthCheck();
var embeddings = openai.AddModel("embeddings", "text-embedding-3-small").WithHealthCheck();

var MarkItDownEndpointName = "http";
var markitdown = builder.AddContainer("markitdown", "mcp/markitdown")
    .WithArgs("--http", "--host", "0.0.0.0", "--port", "3001")
    .WithHttpEndpoint(targetPort: 3001, name: MarkItDownEndpointName);

var postgres = builder.AddAzurePostgresFlexibleServer("postgres")
    .RunAsContainer(x => x.WithPgAdmin());

var postgresDb = postgres.AddDatabase("EmergencyAppDb");

// Azure Communication Services — provisioned via Bicep.
// Role assignment grants the current principal (developer locally, managed identity in ACA)
// Contributor access so DefaultAzureCredential works without a connection string.
var acs = builder.AddBicepTemplate("communication-services", "Bicep/communication-services.bicep")
    .WithParameter(AzureBicepResource.KnownParameters.PrincipalId)
    .WithParameter(AzureBicepResource.KnownParameters.PrincipalType);

var sheltersApi = builder.AddProject<Projects.EmergencyApp_SheltersApi>("shelters-api");

// Azure AI Speech Service — provisioned via Bicep.
// Role assignment grants the Cognitive Services Speech User role to the current principal
// so DefaultAzureCredential can obtain AAD tokens for STT without an API key.
var speech = builder.AddBicepTemplate("speech-service", "Bicep/speech-service.bicep")
    .WithParameter(AzureBicepResource.KnownParameters.PrincipalId)
    .WithParameter(AzureBicepResource.KnownParameters.PrincipalType);

var webApp = builder.AddProject<Projects.EmergencyApp_Web>("aichatweb-app", launchProfileName: "https");
webApp
    .WithReference(chat)
    .WithReference(embeddings)
    .WithReference(postgresDb)
    .WaitFor(postgresDb)
    .WithReference(sheltersApi)
    .WaitFor(sheltersApi)
    .WaitFor(speech)
    .WithEnvironment("ACS_ENDPOINT", acs.GetOutput("endpoint"))
    .WithEnvironment("SPEECH_KEY", speech.GetOutput("key"))
    .WithEnvironment("SPEECH_REGION", speech.GetOutput("location"))
    .WithExternalHttpEndpoints();
webApp
    .WithEnvironment("MARKITDOWN_MCP_URL", markitdown.GetEndpoint(MarkItDownEndpointName));

builder.Build().Run();
