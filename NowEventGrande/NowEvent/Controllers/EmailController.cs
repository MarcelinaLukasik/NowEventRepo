using Microsoft.AspNetCore.Mvc;
using NowEvent.Models;
using NowEvent.Services.EmailService;

namespace NowEvent.Controllers
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
