namespace CSharpUtils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Расширения enum-ов
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// Выбор элементов из enum-а по условию
        /// (для использования без указания имени класса необходимо
        /// указать <code>using static CSharpUtils.Extensions.EnumExt;</code>)
        /// </summary>
        /// <typeparam name="T">Перебираемый enum</typeparam>
        /// <param name="condition">Условие отбора элементов enum-а</param>
        /// <exception cref="ArgumentException"><paramref name="T"/> не является enum-ом</exception>
        public static IEnumerable<T> Where<T>(Func<T, bool> condition)
            where T : struct, IConvertible
        {
            // Проверяем, является ли переданный тип enum-ом
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T должен быть enum-ом!");

            // Выбираем из переданного enum-а значения по указанному условию
            return Enum.GetValues(typeof(T)).Cast<T>().Where(element => condition(element));
        }
    }
}