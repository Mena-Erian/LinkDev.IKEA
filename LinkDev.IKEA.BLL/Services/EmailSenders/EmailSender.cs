using LinkDev.IKEA.DAL.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.EmailSenders
{
    public class EmailSender : IEmailSender
    {
        //vnlo zpnj cnrk ffan

        //Email protocol is ==> SMTP (Simple Mail Transfer Protocol)
        public void SendEmail(Email email)
        {

            var client = new SmtpClient("smtp.gmail.com", 587);// SSL, TLS [should be enable]

            client.EnableSsl = true; // that enable the two ssl and tsl

            //Sender, Receiver

            // username , password  of Sender
            //Gmail ==> Application Password
            client.Credentials = new NetworkCredential("codewithmena@gmail.com", "vnlozpnjcnrkffan");
            client.Send("codewithmena@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
