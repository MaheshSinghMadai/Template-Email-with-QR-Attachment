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
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
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
        public async Task<IActionResult> SendEmailWithAttachment(AppointmentRequest request)
        {
            EmailMetadata emailMetadata = new(request.Email,
            "Appointment Confirmation",
             $"Your visit date is scheduled for {request.AppointmentDateTime:g}. Please find the QR code attached for verification at site.");

            await _emailService.SendWithAttachment(emailMetadata, request.Name, request.AppointmentDateTime);

            return Ok();
        }

        [HttpPost("templateemailwithqrCode")]
        public async Task<IActionResult> SendTemplateEmailWithAttachment(AppointmentRequest request)
        {
            AppointmentRequest model = new(request.Name, request.Email, request.AppointmentDateTime);

            EmailMetadata emailMetadata = new(model.Email, "Appointment Confirmation");

            var template = "Hi <b>@Model.Name</b>, <br><br>" +
            $"Your visit date is scheduled for {request.AppointmentDateTime:g}. Please find the QR code attached for verification at site." +
            "<br><br> Regards,<br> Mahesh";

            await _emailService.SendTemplateEmailWithAttachment(emailMetadata, template, model,  request.Name, request.AppointmentDateTime);

            return Ok();
        }
    }
}
