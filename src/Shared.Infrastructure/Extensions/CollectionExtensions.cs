using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Infrastructure.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsEmptyCollection<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                return true;
            }
            return !list.Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <param name="batchSize"></param>
        public static void BatchInvoke<T>(this IEnumerable<T> list, Action<IEnumerable<T>> action, int batchSize = 1000)
        {
            if (list == null || list.Count() == 0)
            {
                return;
            }

            if (list.Count() < batchSize)
            {
                action(list);
                return;
            }

            bool isLast = false;
            List<T> temp = new List<T>();
            int total = list.Count();
            int index = 0;

            foreach (var entity in list)
            {
                temp.Add(entity);
                isLast = index == total - 1;
                index++;

                if (index % batchSize == 0 || isLast)
                {
                    action(temp);
                    temp.Clear();
                }
            }
        }
    }
}
