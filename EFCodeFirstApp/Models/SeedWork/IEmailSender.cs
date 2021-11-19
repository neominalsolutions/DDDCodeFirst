using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCodeFirstApp.Models.SeedWork
{
    public interface IEmailSender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from">Kimden</param>
        /// <param name="to">kime</param>
        /// <param name="message">mesaj kısmı</param>
        /// <param name="subject">konu</param>
        void SendEmail(string from, string to, string message, string subject);

    }
}
