namespace Messerli.Email.Configuration
{
    public sealed record PickupDirectory
    {
        public PickupDirectory(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString() => Value;
    }
}
