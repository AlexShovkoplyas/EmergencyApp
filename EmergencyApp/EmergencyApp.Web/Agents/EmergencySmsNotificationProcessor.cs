using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace EmergencyApp.Web.Agents;

public class EmergencySmsNotificationProcessor
{
    private readonly AIAgent _summarizerAgent;
    private readonly NotificationService _notificationService;

    public EmergencySmsNotificationProcessor([FromKeyedServices(AgentNames.SummarizeAgent)] AIAgent summarizerAgent, NotificationService notificationService)
    {
        _summarizerAgent = summarizerAgent;
        _notificationService = notificationService;
    }

    public async Task ProcessNotificationAsync(List<ChatMessage> history, string type)
    {
        string summary = "No history available.";
        if (history != null && history.Count > 0)
        {
            // Create a summarization prompt
            // We clone the list to avoid modification issues during enumeration if the main thread is adding to it
            var messages = new List<ChatMessage>(history);
            messages.Add(new ChatMessage(ChatRole.User, "Summarize the dangerous situation in 2-3 sentences for emergency responders based on the chat history above."));

            try
            {
                var response = await _summarizerAgent.RunAsync(messages);
                summary = response.ToString();
            }
            catch (Exception ex)
            {
                summary = $"Error generating summary: {ex.Message}";
            }
        }

        _notificationService.SendNotification(summary, type);
    }
}
