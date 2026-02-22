namespace EmergencyApp.Web.Services;

public sealed class SpeechTokenService
{
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;

    public SpeechTokenService(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
    }

    // Read lazily so values are correct even when the singleton is created before
    // Aspire finishes injecting the Bicep outputs as environment variables.
    private string Key => _config["SPEECH_KEY"] ?? string.Empty;
    private string Region => _config["SPEECH_REGION"] ?? string.Empty;

    public bool IsConfigured => !string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Region);

    /// <summary>
    /// Exchanges the Speech Service subscription key for a short-lived STS token
    /// (valid ~10 minutes) that the browser-side Azure Speech JS SDK accepts via
    /// <c>SpeechConfig.fromAuthorizationToken(token, region)</c>.
    /// The key never leaves the server; only the ephemeral STS token is sent to the client.
    /// </summary>
    public async Task<SpeechTokenInfo> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        var key = Key;
        var region = Region;

        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(region))
            throw new InvalidOperationException(
                "Azure Speech Service is not configured. Ensure SPEECH_KEY and SPEECH_REGION environment variables are set.");

        using var client = _httpClientFactory.CreateClient();
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"https://{region}.api.cognitive.microsoft.com/sts/v1.0/issueToken");
        request.Headers.Add("Ocp-Apim-Subscription-Key", key);

        var response = await client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var stsToken = await response.Content.ReadAsStringAsync(cancellationToken);
        return new SpeechTokenInfo(Token: stsToken, Region: region);
    }
}

public record SpeechTokenInfo(string Token, string Region);
