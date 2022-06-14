using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendMail(string email, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("thakreshubh782@gmail.com", "shubh@12345");


                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("thakreshubh782@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = $"http://localhost:4200/resetPassword/{token}";
                client.Send(msgObj);
            }
        }
    }
}
