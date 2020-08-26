using System.Collections.Generic;

namespace Messerli.Email
{
    public sealed class EmailMessage
    {
        internal EmailMessage(
            string subject,
            MailboxAddress sender,
            IEnumerable<MailboxAddress> recipients,
            IEnumerable<MailboxAddress> carbonCopyRecipients,
            IEnumerable<MailboxAddress> blindCarbonCopyRecipients,
            IEnumerable<BodyPart> bodyParts,
            AutoGenerationType autoGenerationType)
        {
            Subject = subject;
            Sender = sender;
            Recipients = recipients;
            CarbonCopyRecipients = carbonCopyRecipients;
            BlindCarbonCopyRecipients = blindCarbonCopyRecipients;
            BodyParts = bodyParts;
            AutoGenerationType = autoGenerationType;
        }

        public string Subject { get; }

        public MailboxAddress Sender { get; }

        public IEnumerable<MailboxAddress> Recipients { get; }

        public IEnumerable<MailboxAddress> CarbonCopyRecipients { get; }

        public IEnumerable<MailboxAddress> BlindCarbonCopyRecipients { get; }

        public IEnumerable<BodyPart> BodyParts { get; }

        public AutoGenerationType AutoGenerationType { get; }
    }
}
