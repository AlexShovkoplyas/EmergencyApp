namespace EmergencyApp.Web.Agents;

public class NotificationService
{
    public void SendNotification(string summary, string type)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n\n!!! SMS SENT ({type}) !!!");
        Console.WriteLine($"Summary: {summary}\n");
        Console.ForegroundColor = originalColor;
    }
}
