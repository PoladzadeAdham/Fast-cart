using MailKit.Net.Smtp;
using fastkart101.Abstractions;
using fastkart101.ViewModel.EmailSenderViewModel;
using MimeKit;

namespace fastkart101.Services
{
    public class EmailService : IEmailService
    {
        private SmtpSettingsVm _smtpSettings;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettingsVm>() ?? new();
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(email, email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using SmtpClient smtpClient = new();

                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port);

                await smtpClient.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await smtpClient.SendAsync(message);


            }
            catch (Exception)
            {

                throw;
            }


        }




    }
}
