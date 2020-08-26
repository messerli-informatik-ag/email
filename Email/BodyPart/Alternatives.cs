using System.Collections.Generic;

namespace Messerli.Email.BodyPart
{
    /// <summary>
    /// Gives different alternatives of the same information. E.g. a <see cref="Html" /> and a <see cref="Plain" /> body
    /// part with the same information (apart from styling).
    /// The mail client (or the user) can then choose which to display. Elements later in the list take precedence over
    /// previous parts. <a href="https://www.freesoft.org/CIE/RFC/1521/18.htm">Source</a>.
    /// </summary>
    public sealed class Alternatives : IBodyPartVariant
    {
        public Alternatives(params IBodyPartVariant[] children)
        {
            Children = children;
        }

        public Alternatives(IEnumerable<IBodyPartVariant> children)
        {
            Children = children;
        }

        public IEnumerable<IBodyPartVariant> Children { get; }
    }
}
