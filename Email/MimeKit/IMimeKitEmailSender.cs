using System.Threading.Tasks;
using MimeKit;

namespace Messerli.Email.MimeKit
{
    public interface IMimeKitEmailSender
    {
        public Task SendMail(MimeMessage message);
    }
}
