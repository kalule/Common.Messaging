namespace Common.Messaging.Models
{
    public class EmailRequest
    {
        public List<string> Recipients { get; set; }
        public string Subject { get; set; } 
        public string BodyHtml { get; set; } 
        public List<EmailAttachment> Attachments { get; set; }
    }
}
