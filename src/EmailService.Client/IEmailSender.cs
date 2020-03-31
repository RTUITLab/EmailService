using System.Threading.Tasks;

namespace RTUITLab.EmailService.Client
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
