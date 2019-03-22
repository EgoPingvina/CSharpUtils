namespace CSharpUtils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Методы-расширения к используемым моделям
    /// </summary>
    public static class TypeExt
    {
        /// <summary>
        /// Возвращает коллекцию типов, унаследованных от <paramref name="parentType"/>
        /// </summary>
        /// <param name="parentType">Родительский тип</param>
        /// <param name="assemblyNames">Имена сборок текущего проекта, в которых необходимо произвести поиск
        /// (если не указаны, ищем в сборке, в которой определён <paramref name="parentType"/>)</param>
        /// <returns>Коллекция типов, являющихся наследниками <paramref name="parentType"/>,
        /// или null, если таковые не найдены</returns>
        public static List<Type> GetInheritedTypes(this Type parentType, FindMode finedMode = FindMode.All, List<string> assemblyNames = null)
            => (assemblyNames != null
                ? AppDomain.CurrentDomain
                           .GetAssemblies()
                           .Where(a => assemblyNames.Contains(a.FullName))
                           ?.SelectMany(a => a.GetTypes())
                : Assembly.GetAssembly(parentType)
                          .GetTypes())
                    ?.Where(t => t.IsSubclassOf(parentType)     // Является ли наследником
                        && OptionalChecking(t, finedMode))
                    ?.ToList();

        /// <summary>
        /// Опциональная проверка при передаче <typeparamref name="FindMode"/> в GetInheritedTypes
        /// </summary>
        private static readonly Func<Type, FindMode, bool> OptionalChecking = 
            (t, m) =>
            {
                if ((m & FindMode.All) == FindMode.All)     // Поиск всех наследников
                    return true;

                bool result = true;

                if ((m & FindMode.Class) == FindMode.Class)
                    result = result && t.IsClass;           // Является ли классом (опционально)
                if ((m & FindMode.Enum) == FindMode.Enum)
                    result = result && t.IsEnum;            // Является ли перечислением (опционально)
                if ((m & FindMode.Interface) == FindMode.Interface)
                    result = result && t.IsInterface;       // Является ли интерфейсом (опционально)
                if ((m & FindMode.GenericType) == FindMode.GenericType)
                    result = result && t.IsGenericType;     // Является ли джинериком (опционально)

                return result;
            };

        /// <summary>
        /// Дополнительные режимы поиска типов (можно комбинировать, используя одинарный "или")
        /// </summary>
        [Flags]
        public enum FindMode
        {
            /// <summary>
            /// Брать все типы (По умолчанию)
            /// </summary>
            All = 0x1,

            /// <summary>
            /// Брать, если является классом
            /// </summary>
            Class = 0x2,

            /// <summary>
            /// Брать, если является перечислением
            /// </summary>
            Enum = 0x4,

            /// <summary>
            /// Брать, если является интерфейсом
            /// </summary>
            Interface = 0x8,

            /// <summary>
            /// Брать, если является общим типом(джинерик)
            /// </summary>
            GenericType = 0x16
        }
    }
}