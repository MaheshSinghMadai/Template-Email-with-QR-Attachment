using FluentEmail.Core;
using FluentEmailDemo.Entity;

namespace FluentEmailDemo.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail fluentEmail;
        public EmailService(IFluentEmail fluentEmail)
        {
            this.fluentEmail = fluentEmail ?? throw new ArgumentNullException(nameof(fluentEmail));
        }
        public async Task SendEmail(EmailMetadata emailMetadata)
        {
            await fluentEmail.To(emailMetadata.ToAddress)
                                   .Subject(emailMetadata.Subject)
                                   .Body(emailMetadata.Body)
                                   .SendAsync();
        }
    }
}
