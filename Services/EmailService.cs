using EmailAPI.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        public IConfiguration Configuration { get; }
        public EmailContext _context;


        public EmailService(IConfiguration configuration,EmailContext context)
        {
            Configuration = configuration;
            _context = context;
        }

        public EmailConfiguration GetConfiguration()
        {
            EmailConfiguration config = new EmailConfiguration();
            config.SmtpPassword = Configuration.GetEmailConfiguration("SmtpPassword");
            config.SmtpServer = Configuration.GetEmailConfiguration("SmtpServer");
            config.SmtpPort =   int.Parse(Configuration.GetEmailConfiguration("SmtpPort"));
            config.SmtpEmail = Configuration.GetEmailConfiguration("SmtpEmail");
            return config;
        }

        public async Task<SendStatus> SendEmailAsync(Message message)
        {
            var config = GetConfiguration();
            using var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Email", config.SmtpEmail));
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Body };
            
            List<SentMessage> emailList = new List<SentMessage>();


            using var client = new SmtpClient();
            try
            {
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync(config.SmtpServer, config.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);
                    await client.AuthenticateAsync(config.SmtpEmail, config.SmtpPassword);
            }
            catch (Exception ex)
            {
                List<string> errors = new List<string>();
                foreach (var recipient in message.Recipients)
                {
                    SentMessage email = new SentMessage()
                    {
                        Body = message.Body,
                        Subject = message.Subject,
                        Recipient = recipient,
                        EmailSent = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"),
                        Result = "Failed",
                        FailedMessage = ex.Message
                    };
                    emailList.Add(email);
                }
               await _context.Emails.AddRangeAsync(emailList);
               await _context.SaveChangesAsync();
               await client.DisconnectAsync(true);
               errors.Add(ex.Message);
               if(ex.InnerException != null)
                    errors.Add(ex.InnerException.Message);

               return new SendStatus { Succeed = false,Errors=errors };
            }

            foreach (var recipient in message.Recipients)
            {
                var address = new MailboxAddress("", recipient);

                mimeMessage.To.Add(address);

                SentMessage email = new SentMessage()
                {
                    Body = message.Body,
                    Subject = message.Subject,
                    Recipient = recipient,
                    EmailSent = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"),
                    Result = "OK",
                    FailedMessage=""
                };


                try
                {
                    await client.SendAsync(mimeMessage);
                }
                catch (Exception ex)
                {
                    email.Result = "Failed";
                    email.FailedMessage = ex.Message;
                }
                finally
                {
                    mimeMessage.To.Remove(address);
                    emailList.Add(email);
                }
            }

            await _context.Emails.AddRangeAsync(emailList);
            await _context.SaveChangesAsync();
            await client.DisconnectAsync(true);
            return new SendStatus();
        }

        public async Task<IEnumerable<SentMessage>> GetSentMessagesAsync()
        {
            return await _context.Emails.ToListAsync();
        }
    }
}
