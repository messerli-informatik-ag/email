using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Messerli.Email.Configuration;
using Messerli.FileSystem;
using MimeKit;

namespace Messerli.Email.MimeKit
{
    internal sealed class PickupDirectoryMimeEmailSender : IMimeKitEmailSender
    {
        private const string EmailFileExtension = "eml";

        private readonly PickupDirectory _pickupDirectory;

        private readonly IFileSystem _fileSystem;

        private readonly IFileOpeningBuilder _fileOpeningBuilder;

        public PickupDirectoryMimeEmailSender(
            PickupDirectory pickupDirectory,
            IFileSystem fileSystem,
            IFileOpeningBuilder fileOpeningBuilder)
        {
            _pickupDirectory = pickupDirectory;
            _fileSystem = fileSystem;
            _fileOpeningBuilder = fileOpeningBuilder;
        }

        public async Task SendMail(MimeMessage message)
        {
            EnsurePickupDirectoryExists();
            #if ASYNC_DISPOSABLE_STREAM
            var stream = OpenPickupFile(message);
            await using var disposable = stream;
            #else
            using var stream = OpenPickupFile(message);
            #endif
            await message.WriteToAsync(stream).ConfigureAwait(false);
        }

        private Stream OpenPickupFile(MimeMessage message)
            => _fileOpeningBuilder
                .Create()
                .Write()
                .Open(GetPickupFilePath(message));

        private string GetPickupFilePath(MimeMessage message)
            => Path.Combine(_pickupDirectory.Value, GetPickupFilename(message));

        private static string GetPickupFilename(MimeMessage message)
        {
            var date = MapDateToFileNameFriendlyFormat(message.Date);
            var subject = MapSubjectToFileNameFriendlyFormat(message.Subject);
            return $"{date}-{subject}.{EmailFileExtension}";
        }

        private static string MapSubjectToFileNameFriendlyFormat(string subject)
            => subject
                .Replace(' ', '-')
                .Replace(".", string.Empty)
                .Replace(",", string.Empty);

        private static string MapDateToFileNameFriendlyFormat(DateTimeOffset dateTime)
        {
            // Example: 2020-03-24T11:21:46+00:00 (Source: https://en.wikipedia.org/wiki/ISO_8601)
            const string iso8601FormatSpecifier = "o";

            const char timePartSeparator = ':';
            const char fileNameFriendlyTimePartSeparator = '-';

            return dateTime
                .ToString(iso8601FormatSpecifier, CultureInfo.InvariantCulture)
                .Replace(timePartSeparator, fileNameFriendlyTimePartSeparator);
        }

        private void EnsurePickupDirectoryExists() => _fileSystem.CreateDirectory(_pickupDirectory.Value);
    }
}
