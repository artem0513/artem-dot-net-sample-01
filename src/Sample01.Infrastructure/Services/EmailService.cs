using Microsoft.Extensions.Configuration;
using Sample01.Application.Core.Models;
using Sample01.Application.Core.Services;
using System.Net;
using System.Net.Mail;

namespace Sample01.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerService _logger;

    public EmailService(
        IConfiguration configuration,
        ILoggerService logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void SendEmail(Email email)
    {
        try
        {
            if (!IsValidEmail(email))
            {
                return;
            }

            using var smtpClient = CreateSmtpClient();
            using var mailMessage = CreateMailMessage(email);

            smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }

    private bool IsValidEmail(Email email)
    {
        return !string.IsNullOrWhiteSpace(email.From)
            && !string.IsNullOrWhiteSpace(email.To);
    }

    private SmtpClient CreateSmtpClient()
    {
        var smtpClient = new SmtpClient();

        var deliveryMethod = _configuration["Smtp:DeliveryMethod"];

        if (deliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory.ToString())
        {
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            smtpClient.PickupDirectoryLocation = _configuration["Smtp:PickupDirectoryLocation"];
        }
        else if (deliveryMethod == SmtpDeliveryMethod.Network.ToString())
        {
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = _configuration["Smtp:Host"];
            smtpClient.Port = int.Parse(_configuration["Smtp:Port"]!);
            smtpClient.EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]!);

            smtpClient.Credentials = new NetworkCredential(
                _configuration["Smtp:UserName"],
                _configuration["Smtp:Password"]);
        }

        return smtpClient;
    }

    private MailMessage CreateMailMessage(Email email)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(email.From),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true
        };

        AddMailAddresses(mailMessage.To, email.To);
        AddMailAddresses(mailMessage.CC, email.Cc);
        AddMailAddresses(mailMessage.Bcc, email.Bcc);

        AddAttachments(mailMessage, email);

        return mailMessage;
    }

    private void AddMailAddresses(MailAddressCollection collection, string? addresses)
    {
        if (string.IsNullOrWhiteSpace(addresses))
        {
            return;
        }

        var mailAddresses = addresses
            .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var address in mailAddresses)
        {
            collection.Add(address);
        }
    }

    private void AddAttachments(MailMessage mailMessage, Email email)
    {
        if (email.Attachments == null || email.Attachments.Count == 0)
        {
            return;
        }

        foreach (var attachment in email.Attachments)
        {
            mailMessage.Attachments.Add(attachment.File);
        }
    }
}