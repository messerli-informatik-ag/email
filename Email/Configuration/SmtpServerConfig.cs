using Funcky.Monads;

namespace Messerli.Email.Configuration
{
    public sealed record SmtpServerConfig
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
    }
}
