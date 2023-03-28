using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Email
{
    public class Email : IEmail
    {
        public void EmailSender(string subject, string body, string receiver)
        {
            MailMessage mailMessage = new("jimcary630@gmail.com", receiver) { Subject = subject, Body = body, IsBodyHtml = true };
            SmtpClient smtpClient = new("smtp.gmail.com", 587) { Credentials = new NetworkCredential("jimcary630@gmail.com", "nejzcbsfvmnzlabw"), EnableSsl = true };
            smtpClient.Send(mailMessage);
        }
    }
}
