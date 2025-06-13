namespace Common.Messaging.Models
{
    public class EmailAttachment
    { 
        public string FileName { get; set; } = default!;
        public string FileBase64Content { get; set; } = default!;
        public string? ContentType { get; set; }
    }
}
