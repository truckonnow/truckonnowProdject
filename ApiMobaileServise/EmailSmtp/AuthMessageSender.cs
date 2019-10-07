using DaoModels.DAO.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiMobaileServise.EmailSmtp
{
    public class AuthMessageSender
    {

        public async Task Execute(string email, string subject, string body, List<VehiclwInformation> vehiclwInformation = null, Shipping shipping = null)
        {
            try
            {
                int cId = 1;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress("chuprina.r.v@gmail.com")
                };
                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                if (vehiclwInformation != null)
                {
                    foreach (var vech in vehiclwInformation)
                    {
                        Attachment attachment = new Attachment(vech.Scan.path);
                        attachment.ContentId = cId.ToString();
                        mail.Attachments.Add(attachment);
                        cId++;
                    }
                }
                if (shipping.AskFromUser != null && shipping.AskFromUser.App_will_ask_for_signature_of_the_client_signature != null)
                {
                    Attachment attachment = new Attachment(shipping.AskFromUser.App_will_ask_for_signature_of_the_client_signature.path);
                    attachment.ContentId = cId.ToString();
                    mail.Attachments.Add(attachment);
                    cId++;
                }
                if (shipping.askForUserDelyveryM != null && shipping.askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature != null)
                {
                    Attachment attachment = new Attachment(shipping.askForUserDelyveryM.App_will_ask_for_signature_of_the_client_signature.path);
                    attachment.ContentId = cId.ToString();
                    mail.Attachments.Add(attachment);
                    cId++;
                }
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