using Microsoft.Extensions.DependencyInjection;
using System;

namespace RTUITLab.EmailService.Client
{
    public static class EmailSenderExtension
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services, EmailSenderOptions emailSenderOptions)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHttpClient(HttpEmailSender.HttpClientName, cfg =>
            {
                cfg.BaseAddress = new Uri(emailSenderOptions.BaseAddress);
                cfg.DefaultRequestHeaders.Add("Authorization", emailSenderOptions.Key);
            });
            return services.AddTransient<IEmailSender, HttpEmailSender>();
        }
    }
}
