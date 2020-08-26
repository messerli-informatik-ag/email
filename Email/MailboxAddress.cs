namespace Messerli.Email
{
    public sealed class MailboxAddress
    {
        public MailboxAddress(string address, string? name = null)
        {
            Address = address;
            Name = name;
        }

        public string Address { get; }

        public string? Name { get; }
    }
}
