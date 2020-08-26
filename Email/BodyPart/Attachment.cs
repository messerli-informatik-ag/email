using System;
using System.IO;
using System.Net.Mime;

namespace Messerli.Email.BodyPart
{
    public sealed class Attachment : IBodyPartVariant
    {
        public Attachment(ContentType contentType, string fileName, Func<Stream> streamFactory)
        {
            ContentType = contentType;
            FileName = fileName;
            StreamFactory = streamFactory;
        }

        public ContentType ContentType { get; }

        public string FileName { get; }

        public Func<Stream> StreamFactory { get; }
    }
}
