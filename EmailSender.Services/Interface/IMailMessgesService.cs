using EmailSender.Domain.DTO;
using EmailSender.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Services.Interface
{
    public interface IMailMessgesService
    {
        bool SaveMailSubjects(MailMessages message);
        List<MailMessages> GetMailSubjets();
        MailMessages GetMailSubjetById(int SubjectId);
        bool SendMailsAsync(MailDTO mailDTO);
    }
}
