using Funcky;

namespace Messerli.Email.Configuration
{
    [DiscriminatedUnion(NonExhaustive = true)]
    public abstract partial record EmailDelivery
    {
        private EmailDelivery()
        {
        }

        public sealed partial record NullDelivery : EmailDelivery;

        public sealed partial record PickupDelivery : EmailDelivery
        {
            public PickupDelivery(PickupDirectory directory)
            {
                Directory = directory;
            }

            public PickupDirectory Directory { get; }
        }

        public sealed partial record SmtpDelivery : EmailDelivery
        {
            public SmtpDelivery(SmtpServerConfig serverConfig)
            {
                ServerConfig = serverConfig;
            }

            public SmtpServerConfig ServerConfig { get; }
        }
    }
}
