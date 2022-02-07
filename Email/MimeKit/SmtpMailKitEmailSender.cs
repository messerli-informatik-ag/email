using System.Threading.Tasks;
using Funcky.Monads;
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
            await OpenSmtpConnection(client).ConfigureAwait(false);
            await client.SendAsync(message).ConfigureAwait(false);
            await Disconnect(client).ConfigureAwait(false);
        }

        private static async Task Disconnect(IMailService client)
        {
            const bool sendQuitCommandToServer = true;
            await client.DisconnectAsync(sendQuitCommandToServer).ConfigureAwait(false);
        }

        private async Task OpenSmtpConnection(IMailService client)
        {
            await client.ConnectAsync(_smtpServerConfig.Host, _smtpServerConfig.Port, _smtpServerConfig.UseSsl).ConfigureAwait(false);

            await _smtpServerConfig.Credentials.AndThen(async credentials =>
                await client.AuthenticateAsync(credentials.Username, credentials.Password).ConfigureAwait(false));
        }
    }
}
