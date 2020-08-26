#pragma warning disable CS0660, CS0661

namespace Messerli.Email.Configuration
{
    /// <summary>
    /// "Sends" the emails by saving them as .eml files into a given <see cref="Directory" />.
    /// </summary>
    [Equals]
    public readonly struct PickupDelivery : IEmailDeliveryVariant
    {
        public readonly PickupDirectory Directory;

        public PickupDelivery(PickupDirectory directory)
        {
            Directory = directory;
        }

        public static bool operator ==(PickupDelivery left, PickupDelivery right) => Operator.Weave(left, right);

        public static bool operator !=(PickupDelivery left, PickupDelivery right) => Operator.Weave(left, right);
    }
}
