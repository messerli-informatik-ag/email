using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace Messerli.Email
{
    public abstract class BodyPart
    {
        private BodyPart()
        {
        }

        internal abstract TResult Match<TResult>(
            Func<Alternatives, TResult> alternatives,
            Func<Attachment, TResult> attachment,
            Func<Html, TResult> html,
            Func<Plain, TResult> plain);

        /// <summary>
        /// Gives different alternatives of the same information. E.g. a <see cref="BodyPart.Html" /> and a <see cref="BodyPart.Plain" /> body
        /// part with the same information (apart from styling).
        /// The mail client (or the user) can then choose which to display. Elements later in the list take precedence over
        /// previous parts. <a href="https://www.freesoft.org/CIE/RFC/1521/18.htm">Source</a>.
        /// </summary>
        public sealed class Alternatives : BodyPart
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

            internal override TResult Match<TResult>(
                Func<Alternatives, TResult> alternatives,
                Func<Attachment, TResult> attachment,
                Func<Html, TResult> html,
                Func<Plain, TResult> plain) => alternatives(this);
        }

        public sealed class Attachment : BodyPart
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

            internal override TResult Match<TResult>(
                Func<Alternatives, TResult> alternatives,
                Func<Attachment, TResult> attachment,
                Func<Html, TResult> html,
                Func<Plain, TResult> plain) => attachment(this);
        }

        public sealed class Html : BodyPart
        {
            public Html(string content)
            {
                Content = content;
            }

            public string Content { get; }

            internal override TResult Match<TResult>(
                Func<Alternatives, TResult> alternatives,
                Func<Attachment, TResult> attachment,
                Func<Html, TResult> html,
                Func<Plain, TResult> plain) => html(this);
        }

        public sealed class Plain : BodyPart
        {
            public Plain(string content)
            {
                Content = content;
            }

            public string Content { get; }

            internal override TResult Match<TResult>(
                Func<Alternatives, TResult> alternatives,
                Func<Attachment, TResult> attachment,
                Func<Html, TResult> html,
                Func<Plain, TResult> plain) => plain(this);
        }
    }
}
