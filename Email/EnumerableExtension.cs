using System;
using System.Collections.Generic;
using System.Linq;

namespace Messerli.Email
{
    internal static class EnumerableExtension
    {
        public static void DisposeAll<TItem>(this IEnumerable<TItem> enumerable)
            where TItem : IDisposable
        {
            var exceptions = enumerable.SelectMany(DisposeItem).ToList();
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        private static IEnumerable<Exception> DisposeItem<TItem>(TItem item)
            where TItem : IDisposable
        {
            if (ExecuteActionAndReturnException(item.Dispose) is { } exception)
            {
                yield return exception;
            }
        }

        private static Exception? ExecuteActionAndReturnException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                return exception;
            }

            return null;
        }
    }
}
