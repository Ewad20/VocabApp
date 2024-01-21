using System.Net.Mail;
using System.Net;
using System.Text;

namespace ZTPAPP.Models
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;
        SmtpClient client;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;

            client = new SmtpClient(_configuration.GetValue<string>("EmailService:host"), _configuration.GetValue<int>("EmailService:port"));
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration.GetValue<string>("EmailService:login"), _configuration.GetValue<string>("EmailService:password"));
        }

        public void SayHi(string toEmail, string name)
        {
            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.GetValue<string>("EmailService:login"));
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = name;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat($"<p>Hi!!</p>");
            mailMessage.Body = mailBody.ToString();
            // Send email
            client.Send(mailMessage);
        }
    }
}
