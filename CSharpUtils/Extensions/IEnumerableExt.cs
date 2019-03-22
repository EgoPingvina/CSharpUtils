namespace CSharpUtils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExt
    {
        /// <summary>
        /// Преобразование коллекции в строку (вызывает ToString элементов коллекции)
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="source">Коллекция, элементы которой необходимо разместить в одной строке</param>
        /// <param name="separator">Разделитель элементов (по умолчанию-переход на новую строку)</param>
        /// <returns>Строковое представление коллекции</returns>
        public static string CollectionToString<T>(this IEnumerable<T> source, string separator = "\n")
            => string.Join(separator, source.Select(p => p.ToString()).ToArray());

        /// <summary>
        /// Выполняет для каждого элемента коллекции действие
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="collection">Перебираемая коллекция</param>
        /// <param name="action">Действие, выполняемое над каждым элементом</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }

        /// <summary>
        /// Выполняет для каждого элемента коллекции действие с передачей порядково номера текущей итерации
        /// </summary>
        /// <typeparam name="T">Тип элементов коллекции</typeparam>
        /// <param name="collection">Перебираемая коллекция</param>
        /// <param name="handler">Действие, выполняемое над каждым элементом</param>
        public static void ForEachWithIndex<T>(this IEnumerable<T> collection, Action<T, int> handler)
        {
            int index = 0;
            collection.ForEach(item => handler(item, index++));
        }
    }
}