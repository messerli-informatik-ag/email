using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Funcky.Extensions;
using Funcky.Monads;

namespace Messerli.Email
{
    internal static class EnumerableExtension
    {
        public static void DisposeAll<TItem>(this IEnumerable<TItem> enumerable)
            where TItem : IDisposable
        {
            var exceptions = enumerable.WhereSelect(DisposeItem).ToImmutableList();
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        private static Option<Exception> DisposeItem<TItem>(TItem item)
            where TItem : IDisposable
            => ExecuteActionAndReturnException(item.Dispose);

        private static Option<Exception> ExecuteActionAndReturnException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                return exception;
            }

            return Option<Exception>.None();
        }
    }
}
