using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace EmergencyApp.Web.Agents;

public class RootAgentFactory(IChatClient chatClient, EmergencyManager emergencyManager)
{
    public AIAgent Build()
    {
        // Create Agents
        var conversationAgent = AgentFactory.CreateConversationAgent(chatClient);
        var medicalAgent = AgentFactory.CreateMedicalAgent(chatClient);
        var shelterAgent = AgentFactory.CreateShelterAgent(chatClient);
        var emergencyMonitorAgent = AgentFactory.CreateEmergencyMonitorAgent(chatClient, emergencyManager);

        // Build the Handoff Workflow using the built-in Builder
        // This replaces the custom HandoffOrchestrator class
        var workflow = AgentWorkflowBuilder.CreateHandoffBuilderWith(conversationAgent)
            // ConversationAgent can handoff to Medical or Shelter
            .WithHandoff(conversationAgent, medicalAgent, "Handoff to MedicalAgent if the user mentions injury, bleeding, choking, unconsciousness or any medical emergency.")
            .WithHandoff(conversationAgent, shelterAgent, "Handoff to ShelterAgent if the user asks to find a shelter, safe location, or needs navigation.")

            // MedicalAgent can handoff back to Conversation OR directly to Shelter
            .WithHandoff(medicalAgent, conversationAgent, "Handoff to ConversationAgent when the medical issue appears resolved, or the user says they're fine or no longer need medical guidance.")
            .WithHandoff(medicalAgent, shelterAgent, "Handoff to ShelterAgent if the user asks for shelter or safe location while dealing with medical issues.")

            // ShelterAgent can handoff back to Conversation OR directly to Medical
            .WithHandoff(shelterAgent, conversationAgent, "Handoff to ConversationAgent when the user has been successfully navigated to a shelter choice or says they no longer need navigation help.")
            .WithHandoff(shelterAgent, medicalAgent, "Handoff to MedicalAgent if the user mentions a new injury or medical emergency during navigation.")
            .Build();

        // Create a Concurrent Workflow that runs the Handoff workflow and EmergencyMonitor in parallel
        var concurrentWorkflow = AgentWorkflowBuilder.BuildConcurrent([workflow.AsAIAgent(), emergencyMonitorAgent]);

        return concurrentWorkflow.AsAIAgent();
    }
}

