using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Notifications
{
    public interface IEmailService
    {
       public void SendEmail(string toEmail, string subject, string body);
    }
}
