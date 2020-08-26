using System.Threading.Tasks;

namespace Messerli.Email.MimeKit
{
    internal sealed class MimeKitEmailSender : IEmailSender
    {
        private readonly IMimeKitMessageMapper _mimeKitMessageMapper;

        private readonly IMimeKitEmailSender _mimeKitEmailSender;

        public MimeKitEmailSender(IMimeKitMessageMapper mimeKitMessageMapper, IMimeKitEmailSender mimeKitEmailSender)
        {
            _mimeKitMessageMapper = mimeKitMessageMapper;
            _mimeKitEmailSender = mimeKitEmailSender;
        }

        public async Task SendMessage(EmailMessage message)
        {
            using var mimeMessage = _mimeKitMessageMapper.MapToMimeKitMessage(message);
            await _mimeKitEmailSender.SendMail(mimeMessage.Value);
        }
    }
}
