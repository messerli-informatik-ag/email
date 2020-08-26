#pragma warning disable CS0660, CS0661

namespace Messerli.Email.Configuration
{
    [Equals]
    public sealed class SmtpCredentials
    {
        public SmtpCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }

        public string Password { get; }

        public static bool operator ==(SmtpCredentials left, SmtpCredentials right) => Operator.Weave(left, right);

        public static bool operator !=(SmtpCredentials left, SmtpCredentials right) => Operator.Weave(left, right);
    }
}
