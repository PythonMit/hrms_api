using HRMS.Core.Consts;
using HRMS.Core.Models.Email;
using HRMS.Core.Settings;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System;
using HRMS.Core.Exstensions;
using HRMS.Core.Models.Salary;
using Imagekit.Models;
using HRMS.Core.Models.Slack;
using HtmlAgilityPack;

namespace HRMS.Core.Utilities.Notification.Email
{
    public class EmailUtility : IEmailUtility
    {
        private readonly EmailSettings _emailSettings;
        private readonly ApiSettings _apiSettings;
        //private readonly IHttpClientFactory _httpClientFactory;

        public EmailUtility(IOptions<EmailSettings> mailSettings, IOptions<ApiSettings> apiSettings)
        {
            _emailSettings = mailSettings.Value;
            _apiSettings = apiSettings.Value;
        }
        public async Task<(bool reponse, string message)?> SendAsync(EmailOptions options, MailerType type)
        {
            switch (type)
            {
                case MailerType.Smtp:
                    return await SendSmtpAsync(options);
                case MailerType.ThirdParty:
                    return await SendThirdPartyAsync(options);
                default:
                    return (reponse: false, message: string.Empty);
            }
        }

        public string GenerateMessage(EmailOptions options, bool hasHtml = true)
        {
            if (!string.IsNullOrEmpty(options.Template))
            {
                options.Template = options.Template.Replace("%APIURL%", _apiSettings.AppUrl);
                options.Template = options.Template.Replace("%TODAYDATE%", DateTime.Now.ToString("MMMM dd, yyyy"));

                foreach (var item in options.Options.SelectMany(x => x.Parameters).ToList())
                {
                    if (item.Key == "name")
                    {
                        options.Template = options.Template.Replace("%RECIPIENTNAME%", item.Value);
                    }
                    else if (item.Key == "status")
                    {
                        if (item.Value == EmployeeOverTimeStatusType.Approved.GetEnumDescriptionAttribute() || item.Value == EmployeeLeaveStatusType.Approved.GetEnumDescriptionAttribute())
                        {
                            options.Template = options.Template.Replace("%STATUS%", item.Value);
                        }
                        else
                        {
                            options.Template = options.Template.Replace(", totaling ", string.Empty).Replace("%TOTALDAYSHOURS%", string.Empty).Replace("%STATUS%", item.Value);
                        }
                    }
                    else if (item.Key == "dates")
                    {
                        options.Template = options.Template.Replace("%DATES%", item.Value);
                    }
                    else if (item.Key == "totaldayshours")
                    {
                        options.Template = options.Template.Replace("%TOTALDAYSHOURS%", item.Value);
                    }
                    else if (item.Key == "approvedby")
                    {
                        options.Template = options.Template.Replace("%APPROVEDBY%", item.Value);
                    }
                    else if (item.Key == "approvedposition")
                    {
                        options.Template = options.Template.Replace("%APPROVEDPOSITION%", item.Value);
                    }
                }
            }
            return (hasHtml ? options.Template : GetStringWithoutHtml(options.Template));
        }

        private async Task<(bool reponse, string message)?> SendThirdPartyAsync(EmailOptions options)
        {
            if (options.Options == null)
            {
                return (reponse: false, message: string.Empty);
            }
            //var client = _httpClientFactory.CreateClient(_emailSettings.ThirdParty.ClientType);
            //var form = new Dictionary<string, string>();
            //form["from"] = options.FromEmail;

            //var dataDictionary = new Dictionary<string, IDictionary<string, string>>();

            //foreach (var item in options.Options)
            //{
            //    dataDictionary.Add(item.ToMail, item.Parameters);
            //}
            //form["html"] = options.Template;
            //form["recipient-variables"] = System.Text.Json.JsonSerializer.Serialize(dataDictionary);
            //if (options.Options != null && options.Options.Any())
            //{
            //    form["to"] = string.Join(',', options.Options.Select(x => x.ToMail));
            //}

            //form["subject"] = options.Options.FirstOrDefault()?.Subject;
            //if (string.IsNullOrEmpty(options.Template))
            //{
            //    form["template"] = options.TemplateId;
            //}

            //var response = await client.PostAsync(_emailSettings.ThirdParty.ApiBaseUri + _emailSettings.ThirdParty.RequestUri + "/messages", new FormUrlEncodedContent(form));
            //response.EnsureSuccessStatusCode();
            return (reponse: true, message: string.Empty);
        }

        private async Task<(bool reponse, string message)?> SendSmtpAsync(EmailOptions options)
        {
            string to = "", subject = "";
            if (string.IsNullOrEmpty(options.FromEmail))
            {
                return (reponse: false, message: string.Empty);
            }

            foreach (var item in options.Options)
            {
                to += item.ToMail + ";";
                subject += item.Subject;
            }

            var messageBody = GenerateMessage(options);

            MailMessage message = new MailMessage(options.FromEmail, to.Trim(';'));
            message.Subject = subject;
            message.Body = messageBody;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            foreach (var item in _emailSettings.CC?.Split(";"))
            {
                message.CC.Add(item);
            }

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
                client.Host = _emailSettings.Smtp.SmtpServer;
                client.Port = _emailSettings.Smtp.Port;
                await client.SendMailAsync(message);
                client.Dispose();
                return (reponse: true, message: messageBody);
            }
        }

        private string GetStringWithoutHtml(string htmlContent)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);
            var document = html.DocumentNode;

            var nodes = document.SelectNodes("//div[@class='message']");
            var result = string.Empty;
            var t = nodes.Select(x => x.InnerText).FirstOrDefault();
            var s = t.Split(Environment.NewLine, StringSplitOptions.None);
            foreach (var item in s)
            {
                result += (item.Trim() + "");
            }
            if (result.Contains("Thanks,"))
            {
                result = result.Split("Thanks,")[0];
            }
            return result;
        }
    }
}
