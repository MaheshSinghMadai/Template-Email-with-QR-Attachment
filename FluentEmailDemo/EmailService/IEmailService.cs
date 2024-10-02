using FluentEmailDemo.Entity;

namespace FluentEmailDemo.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(EmailMetadata emailMetadata);
        Task SendWithAttachment(EmailMetadata emailMetadata, string name, DateTime appointmentDateTime);
    }
}
