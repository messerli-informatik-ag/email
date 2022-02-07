using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Funcky;

namespace Messerli.Email
{
    [DiscriminatedUnion(NonExhaustive = true)]
    public abstract partial class BodyPart
    {
        private BodyPart()
        {
        }

        /// <summary>
        /// Gives different alternatives of the same information. E.g. a <see cref="BodyPart.Html" /> and a <see cref="BodyPart.Plain" /> body
        /// part with the same information (apart from styling).
        /// The mail client (or the user) can then choose which to display. Elements later in the list take precedence over
        /// previous parts. <a href="https://www.freesoft.org/CIE/RFC/1521/18.htm">Source</a>.
        /// </summary>
        public sealed partial class Alternatives : BodyPart
        {
            public Alternatives(params BodyPart[] children)
            {
                Children = children;
            }

            public Alternatives(IEnumerable<BodyPart> children)
            {
                Children = children;
            }

            public IEnumerable<BodyPart> Children { get; }
        }

        public sealed partial class Attachment : BodyPart
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

        public sealed partial class Html : BodyPart
        {
            public Html(string content)
            {
                Content = content;
            }

            public string Content { get; }
        }

        public sealed partial class Plain : BodyPart
        {
            public Plain(string content)
            {
                Content = content;
            }

            public string Content { get; }
        }
    }
}
