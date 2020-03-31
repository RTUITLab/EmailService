using Newtonsoft.Json;
using RTUITLab.EmailService.PublicAPI.Requests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RTUITLab.EmailService.Client
{
    internal class HttpEmailSender : IEmailSender
    {
        public const string HttpClientName = nameof(HttpEmailSender) + nameof(HttpClientName);
        private readonly HttpClient client;

        public HttpEmailSender(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient(HttpClientName);
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var request = await client.PostAsync("/api/email/send", new StringContent(
                    JsonConvert.SerializeObject(
                            new SendEmailRequest
                            {
                                Email = email,
                                Subject = subject,
                                Body = body
                            }
                        ), Encoding.UTF8, "application/json")
                    );
                if (request.StatusCode == System.Net.HttpStatusCode.OK)
                    return;
                throw new Exception(await request.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while sending email", ex);
            }
        }
    }
}
