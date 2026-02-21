using Azure.Communication.Sms;

namespace EmergencyApp.Web.Services;

public class SmsSender(SmsClient smsClient, IConfiguration configuration, ILogger<SmsSender> logger)
{
    private readonly string _fromPhone = configuration["ACS_SENDER_PHONE_NUMBER"]
        ?? throw new InvalidOperationException(
            "ACS_SENDER_PHONE_NUMBER is not configured. " +
            "Purchase a phone number in the Azure Communication Services portal and set this value.");

    /// <summary>
    /// Sends an SMS message to the given E.164-formatted phone number (e.g. +12345678900).
    /// Returns true if the message was accepted by the carrier, false otherwise.
    /// </summary>
    public async Task<bool> SendAsync(string toPhoneNumber, string message, CancellationToken cancellationToken = default)
    {
        var response = await smsClient.SendAsync(
            from: _fromPhone,
            to: toPhoneNumber,
            message: message,
            cancellationToken: cancellationToken);

        var result = response.Value;

        if (result.Successful)
        {
            logger.LogInformation(
                "SMS sent to {To} (messageId: {MessageId})", toPhoneNumber, result.MessageId);
            return true;
        }

        logger.LogWarning(
            "Failed to send SMS to {To}: {Error}", toPhoneNumber, result.ErrorMessage);
        return false;
    }
}
