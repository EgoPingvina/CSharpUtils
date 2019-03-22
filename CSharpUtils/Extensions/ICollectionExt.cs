namespace CSharpUtils.Extensions
{
    using System.Collections.Generic;

    public static class ICollectionExt
    {
        /// <summary>
        /// Добавление группы новых элементов в конец коллекции, не поддерживающей <code>AddRange</code>
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="collection">Коллекция, в которую добавляются новые элементы</param>
        /// <param name="newItems">Набор добавляемых элементов</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> newItems)
        {
            foreach (var item in newItems)
                collection.Add(item);
        }
    }
}