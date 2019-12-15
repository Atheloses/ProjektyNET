using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Service
{
    class Email
    {

        #region Private

        private readonly ILogger<Worker> _logger;

        #endregion Private

        #region ctor

        public Email(ILogger<Worker> p_Logger)
        {
            _logger = p_Logger;
        }

        #endregion ctor

        #region Public

        public bool SendEmail(string p_Reciever, Dictionary<string,MemoryStream> p_Attachments)
        {
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();
            var client = new SmtpClient();

            try
            {
                message.From.Add(new MailboxAddress("atnet2019@seznam.cz"));
                message.To.Add(new MailboxAddress(p_Reciever));

                message.Subject = "AT.NET Forecast";
                bodyBuilder.HtmlBody = "<h1>Předpověď počasí</h1>";

                if (p_Attachments != null && p_Attachments.Count > 0)
                    foreach (var row in p_Attachments)
                        bodyBuilder.Attachments.Add(row.Key, row.Value.ToArray());

                message.Body = bodyBuilder.ToMessageBody();

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.seznam.cz", 465, true);
                client.Authenticate("atnet2019@seznam.cz", "janousek");
                client.Send(message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTimeOffset.Now}: Error while sending email: {ex}");
                return false;
            }
            finally
            {
                client.Disconnect(true);
            }

            _logger.LogInformation($"{DateTimeOffset.Now}: Email succesfully sent to: {p_Reciever}");
            return true;
        }

        #endregion Public

    }
}
