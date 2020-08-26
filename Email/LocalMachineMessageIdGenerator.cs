using MimeKit.Utils;

namespace Messerli.Email
{
    internal sealed class LocalMachineMessageIdGenerator : IMessageIdGenerator
    {
        public string GenerateMessageId() => MimeUtils.GenerateMessageId();
    }
}
