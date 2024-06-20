using SmtpEmailService;

var emailService = new EmailService();

const string emailTo = "receiverEmail@gmail.com";
const string subject = "testSubject";
const string message = "testMessage";

await emailService.SendEmailAsync(emailTo, subject, message);