using Funcky.Monads;

#pragma warning disable CS0660, CS0661

namespace Messerli.Email.Configuration
{
    [Equals]
    public sealed class SmtpServerConfig
    {
        public SmtpServerConfig(string host, int port, bool useSsl, Option<SmtpCredentials> credentials = default)
        {
            Host = host;
            Port = port;
            UseSsl = useSsl;
            Credentials = credentials;
        }

        public SmtpServerConfig(string host, int port, bool useSsl, SmtpCredentials credentials)
            : this(host, port, useSsl, Option.Some(credentials))
        {
        }

        public string Host { get; }

        public int Port { get; }

        public bool UseSsl { get; }

        public Option<SmtpCredentials> Credentials { get; }

        public static bool operator ==(SmtpServerConfig left, SmtpServerConfig right) => Operator.Weave(left, right);

        public static bool operator !=(SmtpServerConfig left, SmtpServerConfig right) => Operator.Weave(left, right);
    }
}
