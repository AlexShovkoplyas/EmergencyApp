using System;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Protocol;

namespace EmergencyApp.Web.Agents;

public static class DIServices
{
    public static IServiceCollection AddAgents(this IServiceCollection services)
    {
        services.AddScoped<RootAgentFactory>();
        services.AddScoped<EmergencyManager>();
        services.AddScoped<EmergencySmsNotificationProcessor>();
        services.AddScoped<NotificationService>();
        services.AddScoped((sp) => sp.GetRequiredService<RootAgentFactory>().Build());
        services.AddKeyedScoped(AgentNames.SummarizeAgent, (sp,_) => AgentFactory.CreateSummarizerAgent(sp.GetRequiredService<IChatClient>()));
        return services;
    }
}
