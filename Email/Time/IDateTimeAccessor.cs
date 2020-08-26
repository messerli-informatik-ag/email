using System;

namespace Messerli.Email.Time
{
    public interface IDateTimeAccessor
    {
        DateTimeOffset Now { get; }
    }
}
