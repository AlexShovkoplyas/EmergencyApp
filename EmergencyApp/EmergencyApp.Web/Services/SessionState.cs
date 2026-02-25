using System;
using Microsoft.Extensions.AI;

namespace EmergencyApp.Web.Services;

public class SessionState
{
    public List<ChatMessage> Messages { get; set; } = [];
}
