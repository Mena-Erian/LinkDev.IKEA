using LinkDev.IKEA.DAL.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.EmailSenders
{
    public interface IEmailSender
    {
        void SendEmail(Email email);
    }
}
