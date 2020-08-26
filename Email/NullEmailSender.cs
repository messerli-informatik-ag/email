using System.Threading.Tasks;

namespace Messerli.Email
{
    internal sealed class NullEmailSender : IEmailSender
    {
        public Task SendMessage(EmailMessage message) => Task.CompletedTask;
    }
}
