using EmailSender.Domain.DTO;
using EmailSender.Domain.Entities;
using EmailSender.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Controlles
{
    public class HomeController : Controller
    {
        private readonly IMailMessgesService _mailMessgesService;
        public HomeController(IMailMessgesService mailMessgesService)
        {
            _mailMessgesService = mailMessgesService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveSubject([FromBody] MailMessages message)
        {
            var res = _mailMessgesService.SaveMailSubjects(message);
            return Json(new { data = res });
        }  
        public IActionResult GetSubject()
        {
            var res = _mailMessgesService.GetMailSubjets();
            return Json(new { data = res });
        }
        public IActionResult Emails(int id)
        {
            var res = _mailMessgesService.GetMailSubjetById(id);
            return View(res);
        }
      
      
        [HttpPost]
        public IActionResult SendMail([FromBody] MailDTO mailDTO)
        {
            var res = _mailMessgesService.SendMailsAsync(mailDTO);
            return Json(new { data = false });
        }
    }
}
