using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HexaPSA.Tool.Notifications
{
    public static class ServiceCollectionExtension
    {
        public static void AddNotificationsLayer(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IMailConfiguration, MailConfiguration>();

        }
    }
}
