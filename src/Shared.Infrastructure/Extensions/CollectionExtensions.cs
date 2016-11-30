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
        /// <param name="entityList"></param>
        /// <param name="action"></param>
        /// <param name="batchSize"></param>
        public static void BatchOperate<T>(this IEnumerable<T> entityList, Action<IEnumerable<T>> action, int batchSize = 1000)
        {
            if (entityList == null || entityList.Count() == 0)
            {
                return;
            }

            if (entityList.Count() < batchSize)
            {
                action(entityList);
                return;
            }

            bool isLast = false;
            List<T> temp = new List<T>();
            int total = entityList.Count();
            int index = 0;

            foreach (var entity in entityList)
            {
                temp.Add(entity);
                index++;
                isLast = index == total - 1;

                if (index % batchSize == 0 || isLast)
                {
                    action(temp);
                    temp.Clear();
                }
            }
        }
    }
}
