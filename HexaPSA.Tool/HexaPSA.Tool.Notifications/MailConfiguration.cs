using Microsoft.Extensions.Configuration;

namespace HexaPSA.Tool.Notifications
{
    public class MailConfiguration : IMailConfiguration
    {
        private readonly IConfiguration _configuration;

        public MailConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private void EnsureSettingsExist()
        {
            if (string.IsNullOrEmpty(_configuration["SmtpConfig:Host"]) ||
                _configuration.GetValue<int>("SmtpConfig:Port") == 0 ||
                string.IsNullOrEmpty(_configuration["SmtpConfig:Username"]) ||
                string.IsNullOrEmpty(_configuration["SmtpConfig:Password"]) ||
                !_configuration.GetValue<bool>("SmtpConfig:UseSSL") ||
                string.IsNullOrEmpty(_configuration["SmtpConfig:EmailAddress"]) ||
                string.IsNullOrEmpty(_configuration["SmtpConfig:Name"]) ||
                string.IsNullOrEmpty(_configuration["SmtpConfig:AppUrl"]))
            {
                throw new AppSettingsNotFoundException("One or more required settings are missing in appsettings.json");
            }
        }

        public string Host => _configuration["SmtpConfig:Host"];
        public int Port => _configuration.GetValue<int>("SmtpConfig:Port");
        public string Username => _configuration["SmtpConfig:Username"];
        public string Password => _configuration["SmtpConfig:Password"];
        public bool UseSSL => _configuration.GetValue<bool>("SmtpConfig:UseSSL");
        public string EmailAddress => _configuration["SmtpConfig:EmailAddress"];
        public string Name => _configuration["SmtpConfig:Name"];
        public string AppUrl => _configuration["SmtpConfig:AppUrl"];
    }

    public class AppSettingsNotFoundException : Exception
    {
        public AppSettingsNotFoundException(string message) : base(message)
        {
        }
    }
}
