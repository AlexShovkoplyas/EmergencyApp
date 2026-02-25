using Azure;
using Azure.Communication.Email;

namespace EmergencyApp.Web.Services;

public class EmailSender(EmailClient emailClient, IConfiguration configuration, ILogger<EmailSender> logger)
{
    private readonly string _senderAddress = configuration["ACS_SENDER_EMAIL"]
        ?? "DoNotReply@example.com"; // Fallback or throw if critical

    /// <summary>
    /// Sends an email to the given recipient.
    /// Returns true if the message was accepted, false otherwise.
    /// </summary>
    public async Task<bool> SendAsync(string toEmail, string subject, string plainTextContent, string? htmlContent = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var emailMessage = new EmailMessage(
                senderAddress: _senderAddress,
                recipientAddress: toEmail,
                content: new EmailContent(subject)
                {
                    PlainText = plainTextContent,
                    Html = htmlContent
                });

            var operation = await emailClient.SendAsync(WaitUntil.Completed, emailMessage, cancellationToken);
            
            logger.LogInformation("Email sent to {To} (messageId: {MessageId})", toEmail, operation.Id);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send email to {To}", toEmail);
            return false;
        }
    }
}
