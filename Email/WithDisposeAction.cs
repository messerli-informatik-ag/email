using System;
using static Funcky.Functional;

namespace Messerli.Email
{
    internal readonly struct WithDisposeAction<TValue> : IDisposable
    {
        public readonly TValue Value;

        private readonly Action _disposeAction;

        public WithDisposeAction(TValue value, Action disposeAction)
        {
            Value = value;
            _disposeAction = disposeAction;
        }

        public WithDisposeAction(TValue value)
            : this(value, NoOperation)
        {
        }

        public void Dispose() => _disposeAction();
    }
}
