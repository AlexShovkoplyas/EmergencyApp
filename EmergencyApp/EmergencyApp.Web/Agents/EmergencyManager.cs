using EmergencyApp.Web.Services;
using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Timers;
using Timer = System.Timers.Timer;

namespace EmergencyApp.Web.Agents;

public class EmergencyManager(EmergencyNotificationProcessor processor, SessionState sessionState)
{
    private Timer? _timer;
    private readonly object _lock = new();   

    [Description("Sends notification to emergency services. Use immediate=true for immediate life-threatening situations, or immediate=false to delay and batch notifications.")]
    public async Task InitiateSendingNotification(bool immediate)
    {
        if (immediate)
        {
            await ProcessAndSendNotificationAsync("IMMEDIATE");
        }
        else
        {
            ScheduleDelayedNotification();
        }
    }

    private void ScheduleDelayedNotification()
    {
        lock (_lock)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            _timer = new Timer(20000); // 20 seconds
            _timer.Elapsed += async (sender, e) => 
            {
                try 
                {
                    await ProcessAndSendNotificationAsync("DELAYED");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[EmergencyManager] Error in delayed notification: {ex.Message}");
                }
            };
            _timer.AutoReset = false;
            _timer.Start();
            
            Console.WriteLine("\n[EmergencyManager] Notification delayed for 20 seconds...");
        }
    }

    private async Task ProcessAndSendNotificationAsync(string type)
    {
        // Cancel timer if it's running (cleanup)
        lock (_lock)
        {
             if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

        if (sessionState.Messages != null)
        {
             await processor.ProcessNotificationAsync(sessionState.Messages, type);
        }
        else
        {
            Console.WriteLine("\n[EmergencyManager] No history available to summarize.");
        }
    }
}
