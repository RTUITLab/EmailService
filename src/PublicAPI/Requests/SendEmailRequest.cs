using System.ComponentModel.DataAnnotations;

namespace RTUITLab.EmailService.PublicAPI.Requests
{
    public class SendEmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
