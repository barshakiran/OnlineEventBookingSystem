using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEventBookingSystemBL
{
   public class EmailHelper
    { 
        public static readonly string EMAIL_SENDER = "xxxxxxxxxx";   
        public static readonly string EMAIL_CREDENTIALS = "xxxxxxxx"; 
        public static readonly string SMTP_CLIENT = "smtp.gmail.com";   
        public static readonly string EMAIL_BODY = "Event is booked !!";
        public static readonly string EMAIL_SUBJECT = "Event Booking Confirmation";
        private string senderAddress;
        private string clientAddress;
        private string netPassword;

        public EmailHelper()
        {
        }
        public EmailHelper(string sender, string client,string Password)
        {
            senderAddress = sender;
            netPassword = Password;
            clientAddress = client;
        }
       virtual public bool SendEMail(string recipient, string subject, string message)
        {
            bool isMessageSent = false;
            //Intialise Parameters  
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(clientAddress);
            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(senderAddress, netPassword);
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                var mail = new System.Net.Mail.MailMessage(senderAddress.Trim(), recipient.Trim());
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                client.Send(mail);
                isMessageSent = true;
            }
            catch (Exception ex)
            {
                isMessageSent = false;
            }
            return isMessageSent;
        }
    }
}

