using FluentEmail.Core;
using FluentEmailDemo.EmailService;
using FluentEmailDemo.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.QrCode;

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

        [HttpPost("emailwithqrCode")]
        public async Task<IActionResult> SendEmailWithAttachment(string qrStringCode)
        {

            EmailMetadata emailMetadata = new("maheshsinghmadai@gmail.com",
            "FluentEmail Test email",
            "This is a test email from FluentEmail.");

            await _emailService.SendWithAttachment(emailMetadata, qrStringCode);

            return Ok();
        }  
    }
}
