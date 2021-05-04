using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRocky.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public MailJetSettings _mailJetSettings { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task<MailjetResponse> Execute(string email, string subject, string body)
        {
            _mailJetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();

            MailjetClient client = new MailjetClient(_mailJetSettings.ApiKey, _mailJetSettings.SecretKey);
            var request = new MailjetRequest { Resource = Send.Resource }
                 .Property(Send.FromEmail, "rami.saloum@protonmail.com")
                 .Property(Send.FromName, "Rami")
                 .Property(Send.Subject, subject)
                 .Property(Send.HtmlPart, body)
                 .Property(Send.Recipients, new JArray
                  {
                     new JObject
                  {
                     { "Email", email }
                      }
                      });

                       return await client.PostAsync(request);
            //        MailjetRequest request = new MailjetRequest
            //        {
            //            Resource = Send.Resource,
            //        }
            //.Property(Send.Messages, new JArray {
            // new JObject {
            //  {
            //   "From",
            //   new JObject {
            //    {"Email", "marx3k@hotmail.com"},
            //    {"Name", "Rami"}
            //   }
            //  }, {
            //   "To",
            //   new JArray {
            //    new JObject {
            //     {
            //      "Email",
            //      email
            //     }, {
            //      "Name",
            //      "Rami"
            //     }
            //    }
            //   }
            //  }, {
            //   "Subject",
            //   subject
            //  }, {
            //   "HTMLPart",
            //   body
            //  }
            // }
            //});
            //        await client.PostAsync(request);

        }
    }
}
