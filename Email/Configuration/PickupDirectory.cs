namespace Messerli.Email.Configuration
{
    public readonly struct PickupDirectory
    {
        public readonly string Value;

        public PickupDirectory(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }
}
