using System;
using System.Collections.Generic;
using System.Linq;

namespace CpuCommon.Extensions
{
    /// <summary>
    /// Extension methods for collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Performs specified action for each element of the collection.
        /// </summary>
        /// <typeparam name="T">Collection element type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="action">The action for each element.</param>
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            for (int i = 0; i < collection.Count(); i++)
            {
                action(collection.ElementAt(i));
            }
        }
    }
}
