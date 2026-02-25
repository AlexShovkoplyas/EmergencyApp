using System;
using EmergencyApp.Web.Services;
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
        services.AddScoped<EmergencyNotificationProcessor>();
        services.AddScoped<DummyNotificationService>();
        services.AddScoped((sp) => sp.GetRequiredService<RootAgentFactory>().Build());
        services.AddKeyedScoped(AgentNames.SummarizeAgent, (sp,_) => AgentFactory.CreateSummarizerAgent(sp.GetRequiredService<IChatClient>()));
        return services;
    }
}
