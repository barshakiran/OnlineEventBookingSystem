using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEventBookingSystemBL
{
   public class EmailHelper
    {
        // Note: To send email you need to add actual email id and credential so that it will work as expected  
        public static readonly string EMAIL_SENDER = "varshatittusittu@gmail.com"; // change it to actual sender email id or get it from UI input  
        public static readonly string EMAIL_CREDENTIALS = "Barsha@77"; // Provide credentials   
        public static readonly string SMTP_CLIENT = "smtp.gmail.com"; // as we are using outlook so we have provided smtp-mail.outlook.com   
        public static readonly string EMAIL_BODY = "Event is booked !!";
        public static readonly string EMAIL_SUBJECT = "Event Booking Confirmation";
        private string senderAddress;
        private string clientAddress;
        private string netPassword;

        public EmailHelper(string sender, string client,string Password)
        {
            senderAddress = sender;
            netPassword = Password;
            clientAddress = client;
        }
        public bool SendEMail(string recipient, string subject, string message)
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
                //System.Net.Mail.Attachment attachment;  
                //attachment = new Attachment(@"C:\Users\XXX\XXX\XXX.jpg");  
                //mail.Attachments.Add(attachment);  
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

