using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;
using Funcky.Extensions;
using Messerli.Email.BodyPart;

namespace Messerli.Email
{
    public sealed class EmailMessageBuilder
    {
        private const AutoGenerationType DefaultAutoGenerationType = Email.AutoGenerationType.Unspecified;

        private readonly string? _subject;

        private readonly MailboxAddress? _sender;

        private readonly IImmutableList<MailboxAddress> _recipients = ImmutableList<MailboxAddress>.Empty;

        private readonly IImmutableList<MailboxAddress> _carbonCopyRecipients = ImmutableList<MailboxAddress>.Empty;

        private readonly IImmutableList<MailboxAddress> _blindCarbonCopyRecipients = ImmutableList<MailboxAddress>.Empty;

        private readonly IImmutableList<IBodyPartVariant> _bodyParts = ImmutableList<IBodyPartVariant>.Empty;

        private readonly AutoGenerationType? _autoGenerationType;

        public EmailMessageBuilder()
        {
        }

        private EmailMessageBuilder(
            string? subject,
            MailboxAddress? sender,
            IImmutableList<MailboxAddress> recipients,
            IImmutableList<MailboxAddress> carbonCopyRecipients,
            IImmutableList<MailboxAddress> blindCarbonCopyRecipients,
            IImmutableList<IBodyPartVariant> bodyParts,
            AutoGenerationType? autoGenerationType)
        {
            _subject = subject;
            _sender = sender;
            _recipients = recipients;
            _carbonCopyRecipients = carbonCopyRecipients;
            _blindCarbonCopyRecipients = blindCarbonCopyRecipients;
            _bodyParts = bodyParts;
            _autoGenerationType = autoGenerationType;
        }

        [Pure]
        public EmailMessage Build()
        {
            ValidateRecipients();

            return new EmailMessage(
                _subject ?? throw new InvalidOperationException("Missing subject"),
                _sender ?? throw new InvalidOperationException("Missing sender"),
                _recipients,
                _carbonCopyRecipients,
                _blindCarbonCopyRecipients,
                _bodyParts,
                _autoGenerationType ?? DefaultAutoGenerationType);
        }

        [Pure]
        public EmailMessageBuilder Subject(string subject)
            => ShallowClone(subject: subject);

        [Pure]
        public EmailMessageBuilder From(MailboxAddress from)
            => ShallowClone(@from: from);

        [Pure]
        public EmailMessageBuilder AddRecipient(MailboxAddress recipient)
            => ShallowClone(recipients: _recipients.Add(recipient));

        [Pure]
        public EmailMessageBuilder AddRecipients(IEnumerable<MailboxAddress> recipient)
            => ShallowClone(recipients: _recipients.AddRange(recipient));

        [Pure]
        public EmailMessageBuilder AddCarbonCopyRecipient(MailboxAddress recipient)
            => ShallowClone(carbonCopyRecipients: _carbonCopyRecipients.Add(recipient));

        [Pure]
        public EmailMessageBuilder AddCarbonCopyRecipients(IEnumerable<MailboxAddress> recipient)
            => ShallowClone(carbonCopyRecipients: _carbonCopyRecipients.AddRange(recipient));

        [Pure]
        public EmailMessageBuilder AddBlindCarbonCopyRecipient(MailboxAddress recipient)
            => ShallowClone(blindCarbonCopyRecipients: _blindCarbonCopyRecipients.Add(recipient));

        [Pure]
        public EmailMessageBuilder AddBlindCarbonCopyRecipients(IEnumerable<MailboxAddress> recipient)
            => ShallowClone(blindCarbonCopyRecipients: _blindCarbonCopyRecipients.AddRange(recipient));

        [Pure]
        public EmailMessageBuilder AddBodyPart(IBodyPartVariant bodyPart)
            => ShallowClone(bodyParts: _bodyParts.Add(bodyPart));

        [Pure]
        public EmailMessageBuilder AddBodyParts(IEnumerable<IBodyPartVariant> bodyPart)
            => ShallowClone(bodyParts: _bodyParts.AddRange(bodyPart));

        [Pure]
        public EmailMessageBuilder AutoGenerationType(AutoGenerationType? autoGenerationType)
            => ShallowClone(autoGenerationType: autoGenerationType);

        [Pure]
        private EmailMessageBuilder ShallowClone(
            string? subject = null,
            MailboxAddress? from = null,
            IImmutableList<MailboxAddress>? recipients = null,
            IImmutableList<MailboxAddress>? carbonCopyRecipients = null,
            IImmutableList<MailboxAddress>? blindCarbonCopyRecipients = null,
            IImmutableList<IBodyPartVariant>? bodyParts = null,
            AutoGenerationType? autoGenerationType = null)
            => new EmailMessageBuilder(
                subject ?? _subject,
                from ?? _sender,
                recipients ?? _recipients,
                carbonCopyRecipients ?? _carbonCopyRecipients,
                blindCarbonCopyRecipients ?? _blindCarbonCopyRecipients,
                bodyParts ?? _bodyParts,
                autoGenerationType ?? _autoGenerationType);

        private void ValidateRecipients()
        {
            if (_recipients.None() && _carbonCopyRecipients.None() && _carbonCopyRecipients.None())
            {
                throw new InvalidOperationException("There must be at least one recipient");
            }
        }
    }
}
