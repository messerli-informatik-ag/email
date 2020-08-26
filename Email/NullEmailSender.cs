using System.Threading.Tasks;

namespace Messerli.Email
{
    public sealed class NullEmailSender : IEmailSender
    {
        public Task SendMessage(EmailMessage message) => Task.CompletedTask;
    }
}
