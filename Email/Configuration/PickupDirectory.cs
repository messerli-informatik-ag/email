#pragma warning disable CS0660, CS0661

namespace Messerli.Email.Configuration
{
    [Equals]
    public sealed class PickupDirectory
    {
        public PickupDirectory(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static bool operator ==(PickupDirectory left, PickupDirectory right) => Operator.Weave(left, right);

        public static bool operator !=(PickupDirectory left, PickupDirectory right) => Operator.Weave(left, right);

        public override string ToString() => Value;
    }
}
