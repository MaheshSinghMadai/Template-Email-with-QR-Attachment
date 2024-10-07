using FluentEmail.Core;
using FluentEmailDemo.Entity;
using System.Drawing;
using ZXing.QrCode;
using ZXing;
using FluentEmail.Core.Models;

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

        public async Task SendWithAttachment(EmailMetadata emailMetadata, string name, DateTime appointmentDateTime)
        {
            byte[] qrCodeData = GenerateQRCode(name, appointmentDateTime);

            var attachment = new Attachment
            {
                Data = new MemoryStream(qrCodeData),
                Filename = "QRCode.png",
                ContentType = "image/png"
            };

            await fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body, true)
                .Attach(attachment)
                .SendAsync();
        }

        public async Task SendTemplateEmailWithAttachment(EmailMetadata emailMetadata, string template, AppointmentRequest request, string name, DateTime appointmentDateTime)
        {
            byte[] qrCodeData = GenerateQRCode(name, appointmentDateTime);

            var attachment = new Attachment
            {
                Data = new MemoryStream(qrCodeData),
                Filename = "QRCode.png",
                ContentType = "image/png"
            };

            await fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body, true)
                .UsingTemplate(template, request)
                .Attach(attachment)
                .SendAsync();
        }

        public async Task SendTemplateFromDiskEmailWithAttachment(EmailMetadata emailMetadata, string template, AppointmentRequest request, string name, DateTime appointmentDateTime)
        {
            byte[] qrCodeData = GenerateQRCode(name, appointmentDateTime);

            var attachment = new Attachment
            {
                Data = new MemoryStream(qrCodeData),
                Filename = "QRCode.png",
                ContentType = "image/png"
            };

            await fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body, true)
                .UsingTemplateFromFile(template, request)
                .Attach(attachment)
                .SendAsync();
        }

        public byte[] GenerateQRCode(string name, DateTime appointmentDateTime)
        {
            // Combine the data into a single string
            string qrCodeString = $"{name}\nVisitDate: {appointmentDateTime:yyyy-MM-dd HH:mm}";

            var writer = new QRCodeWriter();
            var resultBit = writer.encode(qrCodeString, BarcodeFormat.QR_CODE, 200, 200);
            var matrix = resultBit;
            int scale = 2;
            using (var result = new Bitmap(matrix.Width * scale, matrix.Height * scale))
            {
                for (int x = 0; x < matrix.Height; x++)
                {
                    for (int y = 0; y < matrix.Width; y++)
                    {
                        Color pixel = matrix[x, y] ? Color.Black : Color.White;
                        for (int i = 0; i < scale; i++)
                        {
                            for (int j = 0; j < scale; j++)
                            {
                                result.SetPixel(x * scale + i, y * scale + j, pixel);
                            }
                        }
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}
