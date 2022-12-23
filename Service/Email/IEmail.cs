using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Email
{
    public interface IEmail
    {
        public void EmailSender(string subject, string body, string receiver);
    }
}
