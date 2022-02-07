using System;

namespace Messerli.Email.Configuration
{
    public abstract record EmailDelivery
    {
        private EmailDelivery()
        {
        }

        internal abstract TResult Match<TResult>(
            Func<NullDelivery, TResult> @null,
            Func<PickupDelivery, TResult> pickup,
            Func<SmtpDelivery, TResult> smtp);

        public sealed record NullDelivery : EmailDelivery
        {
            internal override TResult Match<TResult>(
                Func<NullDelivery, TResult> @null,
                Func<PickupDelivery, TResult> pickup,
                Func<SmtpDelivery, TResult> smtp) => @null(this);
        }

        public sealed record PickupDelivery : EmailDelivery
        {
            public PickupDelivery(PickupDirectory directory)
            {
                Directory = directory;
            }

            public PickupDirectory Directory { get; }

            internal override TResult Match<TResult>(
                Func<NullDelivery, TResult> @null,
                Func<PickupDelivery, TResult> pickup,
                Func<SmtpDelivery, TResult> smtp) => pickup(this);
        }

        public sealed record SmtpDelivery : EmailDelivery
        {
            public SmtpDelivery(SmtpServerConfig serverConfig)
            {
                ServerConfig = serverConfig;
            }

            public SmtpServerConfig ServerConfig { get; }

            internal override TResult Match<TResult>(
                Func<NullDelivery, TResult> @null,
                Func<PickupDelivery, TResult> pickup,
                Func<SmtpDelivery, TResult> smtp) => smtp(this);
        }
    }
}
