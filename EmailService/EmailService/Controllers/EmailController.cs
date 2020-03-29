using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using Models.Requests;
using Models.Options;

namespace EmailService.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailSenderOptions options;

        public EmailController(IOptions<EmailSenderOptions> emailSenderOptions)
        {
            this.options = emailSenderOptions.Value;
        }

        private async Task<string> SendEmailAsync(string email, string subject, string message)
        {
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(options.Email));
            mailMessage.To.Add(new MailboxAddress(email));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(options.SmtpHost, options.SmtpPort, SecureSocketOptions.Auto);
                await client.AuthenticateAsync(options.Email, options.Password);

                await client.SendAsync(mailMessage);

                await client.DisconnectAsync(true);
            }
            return email;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendSimpleEmailAsync([FromBody] SendEmailRequest sendEmailRequest)
            => Ok(await SendEmailAsync(sendEmailRequest.Email, sendEmailRequest.Subject, sendEmailRequest.Body));


    }
}