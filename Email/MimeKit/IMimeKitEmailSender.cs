using System.Threading.Tasks;
using MimeKit;

namespace Messerli.Email.MimeKit
{
    internal interface IMimeKitEmailSender
    {
        public Task SendMail(MimeMessage message);
    }
}
