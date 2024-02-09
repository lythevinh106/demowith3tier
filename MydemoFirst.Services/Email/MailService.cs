
using Microsoft.Extensions.Configuration;
using MimeKit;

using Serilog;

namespace MydemoFirst.Services.Email
{
    public class MailService : Contracts.IMail
    {
        private MailKit.Net.Smtp.SmtpClient _smtpClient;

        private readonly IConfiguration _configuration;





        public MailService(MailKit.Net.Smtp.SmtpClient smtpClient,

            IConfiguration configuration
            )
        {

            _smtpClient = smtpClient;
            _configuration = configuration;



            Log.Information(_configuration["Mail"]);
            _smtpClient.Connect(_configuration["Mail:Host"],
                int.Parse(_configuration["Mail:Port"])
                , MailKit.Security.SecureSocketOptions.StartTls);

            _smtpClient.Authenticate(configuration["Mail:MailFrom"], configuration["Mail:PassWord"]);

        }



        public async Task<bool> SendMailAsync(string subject, string body, string emailTo)
        {
            var email = new MimeKit.MimeMessage();

            email.From.Add(MailboxAddress.Parse(_configuration["Mail:MailFrom"]));
            email.To.Add(MailboxAddress.Parse(emailTo));

            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };



            await _smtpClient.SendAsync(email);
            await Disconect();


            return true;
        }


        public async Task<bool> SendMailAsyncHtml(string subject, string contentHtml, string emailTo)
        {
            var email = new MimeKit.MimeMessage();

            email.To.Add(MailboxAddress.Parse(emailTo));

            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = contentHtml
            };



            await _smtpClient.SendAsync(email);
            await Disconect();

            return true;
        }
        public async Task Disconect()
        {
            await _smtpClient.DisconnectAsync(true);
            Log.Information("Disconected Send Mail");
        }




    }
}
