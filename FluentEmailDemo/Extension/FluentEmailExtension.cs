namespace FluentEmailDemo.Extension
{
    public static class FluentEmailExtension
    {
        public static void AddFluentEmail(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["SMTPSetting:Host"];
            var port = emailSettings.GetValue<int>("Port");
            var userName = emailSettings["UserName"];
            var password = emailSettings["Password"];
            var enableSSL = emailSettings.GetValue<bool>("SMTPSetting:EnableSSL");

            services.AddFluentEmail(defaultFromEmail)
                    .AddSmtpSender(host, port,userName,password)
                    .AddRazorRenderer();
        }
    }
}
