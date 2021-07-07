using System.Threading.Tasks;
using MimeKit;

namespace Messerli.Email.MimeKit
{
    internal interface IMimeKitEmailSender
    {
        Task SendMail(MimeMessage message);
    }
}
