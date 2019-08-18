using Administration.Model.Common;
using Administration.Model.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Administration.Model.Utilities
{
    public static class EmailUtils
    {
        public static Result SendEmail(string fromAddress = null, string toAddress = null, string cc = null, string subject = null, string body = null, string emailCredential = null, string passwordCredential = null, string reason = null, string host = null, int port = 0)
        {
            var result = new Result();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(!string.IsNullOrWhiteSpace(fromAddress) ? fromAddress : SystemConfiguration.FromAddress);
                mail.To.Add(toAddress);
                if (!string.IsNullOrWhiteSpace(cc))
                    mail.CC.Add(cc);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient
                {
                    Host = !string.IsNullOrWhiteSpace(host) ? host : SystemConfiguration.EmailHost,
                    Port = port > 0 ? port : SystemConfiguration.EmailPort,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailCredential, passwordCredential),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                smtp.Send(mail);

                result.Code = (short)HttpStatusCode.OK;
                result.Message = string.Format("Send Email successfully!", mail.Subject);

            }
            catch (Exception ex)
            {
                result.Code = (short)HttpStatusCode.BadRequest;
                result.Message = string.Format("Send Email successfully.Please check again!", subject);
                return result;
            }
            return result;

        }
    }
}
