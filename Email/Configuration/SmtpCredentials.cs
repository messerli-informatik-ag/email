namespace Messerli.Email.Configuration
{
    public sealed record SmtpCredentials
    {
        public SmtpCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }

        public string Password { get; }
    }
}
