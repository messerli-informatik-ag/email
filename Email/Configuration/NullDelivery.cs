#pragma warning disable CS0660, CS0661

namespace Messerli.Email.Configuration
{
    [Equals]
    public readonly struct NullDelivery : IEmailDeliveryVariant
    {
        public static bool operator ==(NullDelivery left, NullDelivery right) => Operator.Weave(left, right);

        public static bool operator !=(NullDelivery left, NullDelivery right) => Operator.Weave(left, right);
    }
}
