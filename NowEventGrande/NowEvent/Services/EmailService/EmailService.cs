using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using NowEvent.Models;

namespace NowEvent.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(Email request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(request.EmailAddress));
            email.To.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));

            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text =
               $"<h4><i>email from {request.EmailAddress}</i></h4>" +
               $"<h4><i>Name: {request.FirstName} Surname: {request.LastName}</i></h4>" +
               $"<h4><i>Phone {request.PhoneNumber}</i></h4>" +
               "</br>"+

               $"{request.Message}"
                 };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
