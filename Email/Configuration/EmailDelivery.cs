using System;

#pragma warning disable CS0660, CS0661

namespace Messerli.Email.Configuration
{
    public abstract class EmailDelivery
    {
        private EmailDelivery()
        {
        }

        internal abstract TResult Match<TResult>(
            Func<NullDelivery, TResult> @null,
            Func<PickupDelivery, TResult> pickup,
            Func<SmtpDelivery, TResult> smtp);

        [Equals]
        public sealed class NullDelivery : EmailDelivery
        {
            public static bool operator ==(NullDelivery left, NullDelivery right) => Operator.Weave(left, right);

            public static bool operator !=(NullDelivery left, NullDelivery right) => Operator.Weave(left, right);

            internal override TResult Match<TResult>(
                Func<NullDelivery, TResult> @null,
                Func<PickupDelivery, TResult> pickup,
                Func<SmtpDelivery, TResult> smtp) => @null(this);
        }

        [Equals]
        public sealed class PickupDelivery : EmailDelivery
        {
            public PickupDelivery(PickupDirectory directory)
            {
                Directory = directory;
            }

            public PickupDirectory Directory { get; }

            public static bool operator ==(PickupDelivery left, PickupDelivery right) => Operator.Weave(left, right);

            public static bool operator !=(PickupDelivery left, PickupDelivery right) => Operator.Weave(left, right);

            internal override TResult Match<TResult>(
                Func<NullDelivery, TResult> @null,
                Func<PickupDelivery, TResult> pickup,
                Func<SmtpDelivery, TResult> smtp) => pickup(this);
        }

        [Equals]
        public sealed class SmtpDelivery : EmailDelivery
        {
            public SmtpDelivery(SmtpServerConfig serverConfig)
            {
                ServerConfig = serverConfig;
            }

            public SmtpServerConfig ServerConfig { get; }

            public static bool operator ==(SmtpDelivery left, SmtpDelivery right) => Operator.Weave(left, right);

            public static bool operator !=(SmtpDelivery left, SmtpDelivery right) => Operator.Weave(left, right);

            internal override TResult Match<TResult>(
                Func<NullDelivery, TResult> @null,
                Func<PickupDelivery, TResult> pickup,
                Func<SmtpDelivery, TResult> smtp) => smtp(this);
        }
    }
}
