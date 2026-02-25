using Azure.Communication.Identity;
using Azure.Communication.Sms;
using Azure.Communication.Email;
using Azure.Identity;
using Microsoft.Extensions.AI;
using EmergencyApp.Web.Components;
using EmergencyApp.Web.Data;
using EmergencyApp.Web.Services;
using EmergencyApp.Web.Services.Ingestion;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmergencyApp.Web.Agents;
using EmergencyApp.Web.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddRazorPages();

builder.AddAzureNpgsqlDbContext<ApplicationDbContext>(connectionName: "EmergencyAppDb");

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI();

// Google OAuth (conditionally, if credentials are configured)
var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
if (!string.IsNullOrEmpty(googleClientId) && !string.IsNullOrEmpty(googleClientSecret))
{
    builder.Services.AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = googleClientId;
            options.ClientSecret = googleClientSecret;
        });
}

builder.AddOpenAIClient("chat").AddChatClient();
builder.AddOpenAIClient("embeddings").AddEmbeddingGenerator();

// var vectorStorePath = Path.Combine(AppContext.BaseDirectory, "vector-store.db");
// var vectorStoreConnectionString = $"Data Source={vectorStorePath}";
// builder.Services.AddSqliteVectorStore(_ => vectorStoreConnectionString);
// builder.Services.AddSqliteCollection<string, IngestedChunk>(IngestedChunk.CollectionName, vectorStoreConnectionString);
// builder.Services.AddSingleton<DataIngestor>();
// builder.Services.AddSingleton<SemanticSearch>();
// builder.Services.AddKeyedSingleton("ingestion_directory", new DirectoryInfo(Path.Combine(builder.Environment.WebRootPath, "Data")));

// builder.Services.AddScoped<ChatService>();

builder.Services.AddScoped<UserSettingsService>();
builder.Services.AddScoped<SessionState>();

builder.Services.AddAgents();

builder.Services.AddHttpClient("shelters-mcp", client =>
    client.BaseAddress = new Uri("http://shelters-api/mcp"))
    .AddServiceDiscovery();

builder.Services.AddHttpClient("geo-search-mcp", client =>
    client.BaseAddress = new Uri("http://localhost:5000/sse"));

builder.Services.AddSingleton<GeoSearchAgentFactory>();

builder.Services.AddSingleton<SheltersMcpClientProvider>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<SheltersMcpClientProvider>());

// Azure Communication Services — endpoint injected by Aspire from Bicep output.
// DefaultAzureCredential uses the managed identity in ACA and az-cli credentials locally.
var acsEndpoint = builder.Configuration["ACS_ENDPOINT"];
if (!string.IsNullOrEmpty(acsEndpoint))
{
    var acsCredential = new DefaultAzureCredential();
    var acsUri = new Uri(acsEndpoint);

    builder.Services.AddSingleton(new CommunicationIdentityClient(acsUri, acsCredential));
    builder.Services.AddSingleton(new SmsClient(acsUri, acsCredential));
    builder.Services.AddSingleton<SmsSender>();
    
    // EmailClient usually requires a different endpoint (Data Plane) than the Management Plane or Identity.
    // However, if using the same resource, it might work.
    // Often Email is a separate resource or has a specific endpoint like "https://<resource-name>.unitedstates.communication.azure.com"
    // Let's assume the same endpoint for now, or check if we need a separate configuration.
    // Actually, Email Communication Services is often a separate resource linked to ACS.
    // The endpoint for EmailClient is usually the ACS endpoint.
    builder.Services.AddSingleton(new EmailClient(acsUri, acsCredential));
    builder.Services.AddSingleton<EmailSender>();
}
// Azure Speech Service — endpoint and region injected by Aspire from Bicep outputs.
// DefaultAzureCredential exchanges an AAD token for speech recognition in the browser.
builder.Services.AddSingleton<SpeechTokenService>();


var app = builder.Build();

// Apply EF Core migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
}

// Kick off document ingestion in the background so it's ready before the first chat message
// _ = app.Services.GetRequiredService<SemanticSearch>().LoadDocumentsAsync();

app.MapDefaultEndpoints();
app.AddSpeechApiEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.UseStaticFiles();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

