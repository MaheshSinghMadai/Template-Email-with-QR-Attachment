using FluentEmailDemo.EmailService;
using FluentEmailDemo.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FluentEmailDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService
                ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpGet("singleemail")]
        public async Task<IActionResult> SendSingleEmail()
        {
            EmailMetadata emailMetadata = new("maheshsinghmadai@gmail.com",
            "FluentEmail Test email",
            "This is a test email from FluentEmail.");

            await _emailService.SendEmail(emailMetadata);
            return Ok();
        }
    }
}
