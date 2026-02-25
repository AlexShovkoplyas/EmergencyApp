using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace EmergencyApp.Web.Agents;

public static class AgentFactory
{
    public static AIAgent CreateConversationAgent(IChatClient chatClient)
    {
        string instructions = """
                You are a warm, empathetic assistant helping people in emergency.
                You are the main point of contact and handle general conversation.

                Your responsibilities:
                - Greet the user and understand their situation
                - Provide emotional support and clear communication
                - Recognize when the user needs specialized help and hand off

                Keep your tone calm and reassuring.
                """;

        return new ChatClientAgent(chatClient, instructions, "ConversationAgent");
    }

    public static AIAgent CreateMedicalAgent(IChatClient chatClient)
    {
        string instructions = """
                You are a first aid specialist. You provide clear, calm, step-by-step
                first aid instructions for emergency situations.

                IMPORTANT: ALWAYS ANSWER IN ALL CAPITAL LETTERS.

                Your responsibilities:
                - Ask what the medical situation is if not clear
                - Give numbered, actionable first aid steps
                - Remind the user to call emergency services (112/911) for serious cases
                - Monitor if the situation is resolved

                Keep your tone calm and reassuring. Use numbered steps for instructions.
                """;
        
        return new ChatClientAgent(chatClient, instructions, "MedicalAgent");
    }

    public static AIAgent CreateShelterAgent(IChatClient chatClient)
    {
        string instructions = """
                You are a shelter navigation specialist. You help people find and
                navigate to the nearest safe shelter.

                IMPORTANT: always answer in all lowercase letters.

                Your responsibilities:
                - Ask for the user's location if not provided
                - Present shelter options with distance and key features (electricity, capacity, etc.)
                - Help the user choose and provide Google Maps navigation link
                - Format: https://www.google.com/maps/dir/?api=1&destination=LAT,LNG&travelmode=driving

                Keep your tone calm and reassuring. Use numbered steps for instructions.
                Be practical and concise. Lead with the most important information.
                """;

        return new ChatClientAgent(chatClient, instructions, "ShelterAgent");
    }

    public static AIAgent CreateSummarizerAgent(IChatClient chatClient)
    {
        string instructions = """
                You are an emergency summarizer.
                Your task is to summarize the dangerous situation in 2-3 sentences for emergency responders based on the chat history provided.
                Be concise and focus on the immediate danger and location if available.
                """;

        return new ChatClientAgent(chatClient, instructions, "SummarizerAgent");
    }

    public static AIAgent CreateEmergencyMonitorAgent(IChatClient chatClient, EmergencyManager emergencyManager)
    {
        var sendSmsTool = AIFunctionFactory.Create(emergencyManager.SendSMS);

        var clientWithTools = chatClient.AsBuilder()
            .ConfigureOptions(o => o.Tools = new[] { sendSmsTool })
            .UseFunctionInvocation()
            .Build();

        string instructions = """
                You are a background emergency monitoring system.
                Your sole responsibility is to analyze the conversation for immediate, life-threatening emergencies.
                
                Triggers for SOS:
                - Severe bleeding, unconsciousness, choking, heart attack, stroke.
                - Fire, explosion, active violence, kidnapping.
                - Explicit calls for help like "Call 911", "Help me I'm dying".

                Output Rules:
                - If a life-threatening emergency is detected, call the SendSMS tool.
                - If the situation is extremely urgent and requires immediate intervention (e.g., active bleeding, unconsciousness, fire), set immediate=true.
                - If the situation is serious but allows for a short delay (e.g., broken bone, moderate pain), set immediate=false.
                - Otherwise, output nothing.
                
                Do not provide advice. Do not explain. Only call the tool if necessary.
                """;

        return new ChatClientAgent(clientWithTools, instructions, "EmergencyMonitor");
    }
}
