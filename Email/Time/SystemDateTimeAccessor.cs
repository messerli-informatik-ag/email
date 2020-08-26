using System;

namespace Messerli.Email.Time
{
    internal sealed class SystemDateTimeAccessor : IDateTimeAccessor
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
