using MimeKit;

namespace Messerli.Email.MimeKit
{
    internal interface IMimeKitMessageMapper
    {
        WithDisposeAction<MimeMessage> MapToMimeKitMessage(EmailMessage message);
    }
}
