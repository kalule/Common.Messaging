namespace Common.Messaging.Models
{
    public class EmailRequest
    {
        public string To { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string BodyHtml { get; set; } = default!;
        public List<EmailAttachment> Attachments { get; set; } = new();
    }
    public class EmailRequestedEvent
    {
        public string Subject { get; set; } = "";
        public string HtmlBody { get; set; } = "";
        public List<string> Recipients { get; set; } = new();
        public List<EmailAttachment>? Attachments { get; set; }
    }
}
