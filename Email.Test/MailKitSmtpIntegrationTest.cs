using System.Net.Mime;
using System.Threading.Tasks;
using Messerli.Email.Configuration;
using Xunit;
using static Messerli.Email.Test.IntegrationTestUtility;

namespace Messerli.Email.Test
{
    public sealed class MailKitSmtpIntegrationTest
    {
        private static readonly SmtpServerConfig DefaultMailCatcherConfig =
            new(
                host: "localhost",
                port: 1025,
                useSsl: false);

        [Fact(Skip = "Needs a running MailCatcher instance")]
        public async Task SendMail()
        {
            const string attachmentFileName = "art.jpg";

            var emailSender = CreateEmailSender();
            var message = new EmailMessageBuilder()
                .From(new MailboxAddress("pitcher@localhost", "Pitcher"))
                .AddRecipient(new MailboxAddress("mailcatcher@localhost", "MailCatcher"))
                .Subject("Catch me if you can")
                .AddBodyPart(new BodyPart.Alternatives(
                    new BodyPart.Plain("Hello there"),
                    new BodyPart.Html("<b>Hello there</b>")))
                .AddBodyPart(new BodyPart.Attachment(
                    new ContentType(MediaTypeNames.Image.Jpeg),
                    attachmentFileName,
                    () => OpenResourceFile(attachmentFileName)))
                .Build();

            await emailSender.SendMessage(message);
        }

        private static IEmailSender CreateEmailSender()
            => new EmailSenderBuilder()
                .Build(new EmailDelivery.SmtpDelivery(DefaultMailCatcherConfig));
    }
}
