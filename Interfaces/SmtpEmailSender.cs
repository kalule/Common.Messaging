using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Common.Messaging.Models;

namespace Common.Messaging.Interfaces
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptions<SmtpSettings> smtpOptions, ILogger<SmtpEmailSender> logger)
        {
            _smtpSettings = smtpOptions.Value ?? throw new ArgumentNullException(nameof(smtpOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendAsync(EmailRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    EnableSsl = _smtpSettings.EnableSsl,
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.From),
                    Subject = request.Subject,
                    Body = request.BodyHtml,
                    IsBodyHtml = true
                };

                foreach (var recipient in request.To.Split(',', ';'))
                {
                    if (!string.IsNullOrWhiteSpace(recipient))
                        message.To.Add(recipient.Trim());
                }

                if (request.Attachments != null)
                {
                    foreach (var attachment in request.Attachments)
                    {
                        try
                        {
                            var data = Convert.FromBase64String(attachment.FileBase64Content);
                            var stream = new MemoryStream(data);
                            message.Attachments.Add(new Attachment(stream, attachment.FileName, attachment.ContentType ?? "application/octet-stream"));
                        }
                        catch (Exception attEx)
                        {
                            _logger.LogWarning(attEx, "Failed to process attachment: {FileName}", attachment.FileName);
                        }
                    }
                }

                await client.SendMailAsync(message, cancellationToken);
                _logger.LogInformation("Email sent successfully to: {Recipients}", request.To);
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error occurred while sending email to: {Recipients}", request.To);
                throw; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while sending email to: {Recipients}", request.To);
                throw;
            }
        }
    }
}
