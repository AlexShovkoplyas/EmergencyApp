using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.AI;

namespace EmergencyApp.Web.Services;

public class SessionState(AuthenticationStateProvider authProvider)
{
    public List<ChatMessage> Messages { get; set; } = [];

    public async Task<string> GetCurrentUserId()
    {
        var authState = await authProvider.GetAuthenticationStateAsync();
        var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId;
    }
}
