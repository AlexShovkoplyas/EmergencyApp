using System;
using EmergencyApp.Web.Services;

namespace EmergencyApp.Web.Endpoints;

public static class SpeechApiEndpoints
{
    public static WebApplication AddSpeechApiEndpoints(this WebApplication app)
    {
        app.MapGet("/api/speech/available", (SpeechTokenService s) => Results.Ok(s.IsConfigured));

        app.MapGet("/api/speech/token", async (SpeechTokenService s, CancellationToken ct) =>
        {
            if (!s.IsConfigured)
                return Results.StatusCode(503);
            var info = await s.GetTokenAsync(ct);
            return Results.Ok(info);
        });

        return app;
    }
}
