using Funcky.Monads;

namespace Messerli.Email
{
    public sealed class MailboxAddress
    {
        public MailboxAddress(string address, Option<string> name = default)
        {
            Address = address;
            Name = name;
        }

        public MailboxAddress(string address, string name)
            : this(address, Option.Some(name))
        {
        }

        public string Address { get; }

        public Option<string> Name { get; }
    }
}
