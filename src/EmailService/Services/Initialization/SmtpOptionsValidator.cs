using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Services.Initialization
{
    public class SmtpOptionsValidator : IHostedService
    {
        private readonly IOptions<EmailServiceOptions> emailSenderOptions;
        private readonly ILogger<SmtpOptionsValidator> logger;

        public SmtpOptionsValidator(
            IOptions<EmailServiceOptions> emailSenderOptions,
            ILogger<SmtpOptionsValidator> logger)
        {
            this.emailSenderOptions = emailSenderOptions;
            this.logger = logger;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Checking smtp parameters to {Host} as {Email}", emailSenderOptions.Value.SmtpHost, emailSenderOptions.Value.Email);

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(emailSenderOptions.Value.SmtpHost, emailSenderOptions.Value.SmtpPort, SecureSocketOptions.Auto, cancellationToken);
                await client.AuthenticateAsync(emailSenderOptions.Value.Email, emailSenderOptions.Value.Password, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
                logger.LogInformation("SMTP optinos validated successfully");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Can't connect to STMP host");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
