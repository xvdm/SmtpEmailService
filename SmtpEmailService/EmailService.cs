using MailKit.Net.Smtp;
using MimeKit;

namespace SmtpEmailService;

public sealed class EmailService
{
    public async Task SendEmailAsync(string emailTo, string subject, string message)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(EmailConfiguration.DisplayName, EmailConfiguration.From));
            emailMessage.To.Add(new MailboxAddress("", emailTo));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using var client = new SmtpClient();
            await client.ConnectAsync(EmailConfiguration.Host, EmailConfiguration.Port, true);
            await client.AuthenticateAsync(EmailConfiguration.Login, EmailConfiguration.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}

public static class EmailConfiguration
{
    public const string DisplayName = "displayName";
    public const string From = "senderEmail@gmail.com";
    public const string Host = "smtp-relay.sendinblue.com";
    public const string Login = "smtpLogin";
    public const string Password = "smtpPassword";
    public const int Port = 465;
}