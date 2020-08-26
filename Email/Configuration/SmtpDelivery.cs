#pragma warning disable CS0660, CS0661

namespace Messerli.Email.Configuration
{
    [Equals]
    public readonly struct SmtpDelivery : IEmailDeliveryVariant
    {
        public readonly SmtpServerConfig ServerConfig;

        public SmtpDelivery(SmtpServerConfig serverConfig)
        {
            ServerConfig = serverConfig;
        }

        public static bool operator ==(SmtpDelivery left, SmtpDelivery right) => Operator.Weave(left, right);

        public static bool operator !=(SmtpDelivery left, SmtpDelivery right) => Operator.Weave(left, right);
    }
}
