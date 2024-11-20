using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Domain.DTO
{
    public class MailDTO
    {
        public List<string> Mails { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
