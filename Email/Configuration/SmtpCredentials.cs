namespace Messerli.Email.Configuration
{
    public readonly struct SmtpCredentials
    {
        public readonly string Username;

        public readonly string Password;

        public SmtpCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
