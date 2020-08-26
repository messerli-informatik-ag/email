namespace Messerli.Email.Configuration
{
    public readonly struct SmtpServerConfig
    {
        public readonly string Host;

        public readonly int Port;

        public readonly bool UseSsl;

        public readonly SmtpCredentials? Credentials;

        public SmtpServerConfig(string host, int port, bool useSsl, SmtpCredentials? credentials = null)
        {
            Host = host;
            Port = port;
            UseSsl = useSsl;
            Credentials = credentials;
        }
    }
}
