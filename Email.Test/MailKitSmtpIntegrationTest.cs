using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Messerli.Email.BodyPart;
using Messerli.Email.Configuration;
using Xunit;
using static Messerli.Email.Test.IntegrationTestUtility;

namespace Messerli.Email.Test
{
    public sealed class MailKitSmtpIntegrationTest
    {
        private static readonly SmtpServerConfig DefaultMailCatcherConfig =
            new SmtpServerConfig(
                host: "localhost",
                port: 1025,
                useSsl: false);

        [Fact(Skip = "Needs a running MailCatcher instance")]
        public async Task SendMail()
        {
            const string attachmentFileName = "art.jpg";

            var emailSender = CreateEmailSender();
            var message = new EmailMessageBuilder()
                .From(new MailboxAddress("pitcher@localhost", "Backbone"))
                .AddRecipient(new MailboxAddress("mailcatcher@localhost", "MailCatcher"))
                .Subject("Catch me if you can")
                .AddBodyPart(new Alternatives(
                    new Plain("Hello there"),
                    new Html("<b>Hello there</b>")))
                .AddBodyPart(new Attachment(
                    new ContentType(MediaTypeNames.Image.Jpeg),
                    attachmentFileName,
                    () => OpenResourceFile(attachmentFileName)))
                .Build();

            await emailSender.SendMessage(message);
        }

        private static IEmailSender CreateEmailSender()
            => throw new NotImplementedException();
    }
}
