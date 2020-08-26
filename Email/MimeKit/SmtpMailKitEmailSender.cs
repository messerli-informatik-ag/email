using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using Messerli.Email.Configuration;
using MimeKit;

namespace Messerli.Email.MimeKit
{
    internal sealed class SmtpMailKitEmailSender : IMimeKitEmailSender
    {
        private readonly SmtpServerConfig _smtpServerConfig;

        public SmtpMailKitEmailSender(SmtpServerConfig smtpServerConfig)
        {
            _smtpServerConfig = smtpServerConfig;
        }

        public async Task SendMail(MimeMessage message)
        {
            using var client = new SmtpClient();
            await OpenSmtpConnection(client);
            await client.SendAsync(message);
            await Disconnect(client);
        }

        private static async Task Disconnect(IMailService client)
        {
            const bool sendQuitCommandToServer = true;
            await client.DisconnectAsync(sendQuitCommandToServer);
        }

        private async Task OpenSmtpConnection(IMailService client)
        {
            await client.ConnectAsync(_smtpServerConfig.Host, _smtpServerConfig.Port, _smtpServerConfig.UseSsl);

            if (_smtpServerConfig.Credentials.HasValue)
            {
                var credentials = _smtpServerConfig.Credentials.Value;
                await client.AuthenticateAsync(credentials.Username, credentials.Password);
            }
        }
    }
}
