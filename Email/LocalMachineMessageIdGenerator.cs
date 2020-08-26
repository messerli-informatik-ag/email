using MimeKit.Utils;

namespace Messerli.Email
{
    public sealed class LocalMachineMessageIdGenerator : IMessageIdGenerator
    {
        public string GenerateMessageId() => MimeUtils.GenerateMessageId();
    }
}
