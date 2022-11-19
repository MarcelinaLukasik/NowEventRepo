using System.Reflection.Metadata.Ecma335;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Services.EmailService;

namespace WebApplication2.Controllers
{
    // Used  Mailkit & SMTP
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Hello()
        {
            return Ok();
        }

        [HttpPost("SendEmail")]
        public IActionResult SendEmail([FromBody]Email request)
        {
            _emailService.SendEmail(request);

            return Ok();
        }
    }
}
