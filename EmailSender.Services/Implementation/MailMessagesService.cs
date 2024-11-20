using EmailSender.Domain.Entities;
using EmailSender.Persistence;
using EmailSender.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EmailSender.Domain.DTO;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using Microsoft.Extensions.Options;
using System.Xml.Linq;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Services.Implementation
{
    public class MailMessagesService : IMailMessgesService
    {
        private readonly ApplicationDbContext _context;
        private readonly MailSettings _mailSettings;

        public MailMessagesService(ApplicationDbContext context,MailSettings mailSettings)
        {
            _context = context;
            _mailSettings = mailSettings;
        }

        public MailMessages GetMailSubjetById(int SubjectId)
        {
            var data = _context.MailMessages.FirstOrDefault(x => x.Id == SubjectId);
            return data;
        }

        public List<MailMessages> GetMailSubjets()
        {
            var data = _context.MailMessages.ToList();
            return data;
        }

        public bool SaveMailSubjects(MailMessages message)
        {
            message.AddedDate = DateTime.Now;
            _context.MailMessages.Add(message);
            return _context.SaveChanges() > 0;
        }

        public bool SendMailsAsync(MailDTO mailDTO)
        {
            //change the data  in appsetting.json 

            // Create a new MailMessage object
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_mailSettings.SenderEmail);

            // Add the recipients to the message
            foreach (string toEmail in mailDTO.Mails)
            {
                if (IsValidEmail(toEmail))
                message.To.Add(toEmail);
                return false;
            }

            // Set the subject and body of the message
            message.Subject = mailDTO.Subject;
            message.Body = mailDTO.Message;

            // Create a new SmtpClient object
            SmtpClient client = new SmtpClient();

            // Set the SMTP server and port
            client.Host = _mailSettings.Server;
            client.Port = _mailSettings.Port;
            client.EnableSsl = true;

            // Set the credentials for the sender's email account
            client.Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.Password);

            // Send the message
            client.Send(message);

            return true;

        }

        public bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

    }
}
