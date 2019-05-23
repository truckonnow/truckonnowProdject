using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiMobaileServise.EmailSmtp
{
    public class AuthMessageSender
    {

        public async Task Execute(string email, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress("chuprina.r.v@gmail.com")
                };
                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("chuprina.r.v@gmail.com", "zwhyuebwesopuhmj");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}