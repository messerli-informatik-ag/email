using System.Threading.Tasks;

namespace Messerli.Email
{
    public interface IEmailSender
    {
        Task SendMessage(EmailMessage message);
    }
}
