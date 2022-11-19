using WebApplication2.Models;

namespace WebApplication2.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(Email request);
    }
}
