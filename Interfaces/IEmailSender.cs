using Common.Messaging.Models;

namespace Common.Messaging.Interfaces
{
    /// <summary>
    /// Sends email messages with optional attachments.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email request asynchronously.
        /// </summary>
        /// <param name="request">The email request including recipient, subject, body and attachments.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task SendAsync(EmailRequest request, CancellationToken cancellationToken = default);
    }


}
