using System.Collections.Generic;
using System.Text.Json;

namespace Sprout.Common
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> AsNoTracking<T>(this IEnumerable<T> enumerable) =>
            JsonSerializer.Deserialize<IEnumerable<T>>(JsonSerializer.Serialize(enumerable));
    }
}