using NowEvent.Models;

namespace NowEvent.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(Email request);
    }
}
