using System.Diagnostics.Contracts;
using Messerli.Email.Configuration;
using Messerli.Email.MimeKit;
using Messerli.Email.Time;
using Messerli.FileSystem;

namespace Messerli.Email
{
    public sealed class EmailSenderBuilder
    {
        private readonly IMessageIdGenerator? _messageIdGenerator;

        private readonly IDateTimeAccessor? _dateTimeAccessor;

        private readonly IMultipartBoundaryGenerator? _multipartBoundaryGenerator;

        private readonly IFileSystem? _fileSystem;

        private readonly IFileOpeningBuilder? _fileOpeningBuilder;

        public EmailSenderBuilder()
        {
        }

        public EmailSenderBuilder(
            IMessageIdGenerator? messageIdGenerator,
            IDateTimeAccessor? dateTimeAccessor,
            IMultipartBoundaryGenerator? multipartBoundaryGenerator,
            IFileSystem? fileSystem,
            IFileOpeningBuilder? fileOpeningBuilder)
        {
            _messageIdGenerator = messageIdGenerator;
            _dateTimeAccessor = dateTimeAccessor;
            _multipartBoundaryGenerator = multipartBoundaryGenerator;
            _fileSystem = fileSystem;
            _fileOpeningBuilder = fileOpeningBuilder;
        }

        [Pure]
        public EmailSenderBuilder MessageIdGenerator(IMessageIdGenerator messageIdGenerator)
            => ShallowClone(messageIdGenerator: messageIdGenerator);

        [Pure]
        public EmailSenderBuilder DateTimeAccessor(IDateTimeAccessor dateTimeAccessor)
            => ShallowClone(dateTimeAccessor: dateTimeAccessor);

        [Pure]
        public EmailSenderBuilder MultipartBoundaryGenerator(IMultipartBoundaryGenerator multipartBoundaryGenerator)
            => ShallowClone(multipartBoundaryGenerator: multipartBoundaryGenerator);

        [Pure]
        public IEmailSender Build(EmailDelivery delivery)
            => delivery.Match(
                @null: _ => new NullEmailSender(),
                pickup: CreatePickupDeliverySender,
                smtp: CreateSmtpDeliverySender);

        [Pure]
        internal EmailSenderBuilder FileSystem(IFileSystem fileSystem)
            => ShallowClone(fileSystem: fileSystem);

        [Pure]
        internal EmailSenderBuilder FileOpeningBuilder(IFileOpeningBuilder fileOpeningBuilder)
            => ShallowClone(fileOpeningBuilder: fileOpeningBuilder);

        private EmailSenderBuilder ShallowClone(
            IMessageIdGenerator? messageIdGenerator = null,
            IDateTimeAccessor? dateTimeAccessor = null,
            IMultipartBoundaryGenerator? multipartBoundaryGenerator = null,
            IFileSystem? fileSystem = null,
            IFileOpeningBuilder? fileOpeningBuilder = null)
            => new EmailSenderBuilder(
                messageIdGenerator ?? _messageIdGenerator,
                dateTimeAccessor ?? _dateTimeAccessor,
                multipartBoundaryGenerator ?? _multipartBoundaryGenerator,
                fileSystem ?? _fileSystem,
                fileOpeningBuilder ?? _fileOpeningBuilder);

        private IEmailSender CreatePickupDeliverySender(EmailDelivery.PickupDelivery pickupDelivery)
            => new MimeKitEmailSender(
                CreateMimeKitMessageMapper(),
                new PickupDirectoryMimeEmailSender(
                    pickupDelivery.Directory,
                    CreateFileSystem(),
                    CreateFileOpeningBuilder()));

        private IEmailSender CreateSmtpDeliverySender(EmailDelivery.SmtpDelivery smtpDelivery)
            => new MimeKitEmailSender(
                CreateMimeKitMessageMapper(),
                new SmtpMailKitEmailSender(smtpDelivery.ServerConfig));

        private IMimeKitMessageMapper CreateMimeKitMessageMapper()
            => new MimeKitMessageMapper(
                CreateMessageIdGenerator(),
                CreateDateTimeAccessor(),
                CreateMultipartBoundaryGenerator());

        private IMessageIdGenerator CreateMessageIdGenerator()
            => _messageIdGenerator ?? new LocalMachineMessageIdGenerator();

        private IDateTimeAccessor CreateDateTimeAccessor()
            => _dateTimeAccessor ?? new SystemDateTimeAccessor();

        private IMultipartBoundaryGenerator CreateMultipartBoundaryGenerator()
            => _multipartBoundaryGenerator ?? new RandomMultipartBoundaryGenerator();

        private IFileSystem CreateFileSystem() => _fileSystem ?? new FileSystem.FileSystem();

        private IFileOpeningBuilder CreateFileOpeningBuilder() => _fileOpeningBuilder ?? new FileOpeningBuilder();
    }
}
