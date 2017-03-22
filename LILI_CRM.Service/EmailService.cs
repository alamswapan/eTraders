using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace LILI_BMS.Service
{
    public class EmailService
    {
        private SmtpClient _emailClient;

        public EmailService(string host, int port, string netUser, string password)
        {
            _emailClient = ConfigureClient(host, port, netUser, password);
        }
        public void SendMail(List<string> reciepientList, string subject, string message)
        {

            MailMessage msg = new MailMessage();
            msg.From =
                new MailAddress("ras_el34@yahoo.com.bd", "AutoMail");
            foreach (var item in reciepientList)
            {
                msg.To.Add(item);
            }
            msg.Priority = MailPriority.High;
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;          
            _emailClient.Send(msg);

        }
        private SmtpClient ConfigureClient(string host, int port, string networkUser, string password)
        {
            var client = new SmtpClient();
            client.Host = host;// "smtp.gmail.com";
            //if (port >= 0)
            //{
            //    client.Port = port; // example 578
            //}
            //client.EnableSsl = true; //should be enabled for ssl        
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(networkUser, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            return client;
        }
    }
}
