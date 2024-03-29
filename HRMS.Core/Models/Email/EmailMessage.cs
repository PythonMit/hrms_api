using HRMS.Core.Consts;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace HRMS.Core.Models.Email
{
    public class EmailMessage
    {
        public List<EmailData> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public TemplateType Type { get; set; }
        [JsonIgnore]
        public List<MailboxAddress> MailboxAddress { get; set; }
        public EmailMessage(IEnumerable<EmailData> to)
        {
            MailboxAddress = new List<MailboxAddress>();
            MailboxAddress.AddRange(to.Select(x => new MailboxAddress(x.Name, x.Address)));
        }
    }

    public class EmailData
    {
        public string Address { get; set; }
        public string Name { get; set; }
    }
}
