namespace CSharpUtils.Extensions
{
    using System;

    /// <summary>
    /// Методы-расширения, внедряемые во fluent-последовательности без нарушения результатов и последовательности действий
    /// </summary>
    public static class FluentExt
    {
        /// <summary>
        /// Вызывает событие в произвольном месте fluent-последовательности, не нарушая её
        /// </summary>
        /// <typeparam name="T">Тип результата предыдущего действия в последовательности</typeparam>
        /// <param name="previous">Результат предыдущего во fluent-последовательности метода</param>
        /// <param name="handler">Событие, подписчиков которого необходимо оповестить</param>
        /// <param name="sender">Отправитель(по умолчанию null)</param>
        /// <param name="args">Аргументы(по умолчанию EventArgs.Empty(переприсвоится при args=null))</param>
        /// <returns>Результат выполнения предыдущего метода во fluent-последовательности</returns>
        public static T InvokeEvent<T>(this T previous, EventHandler handler, object sender = null, EventArgs args = null)
        {
            handler?.Invoke(sender, args ?? EventArgs.Empty);
            return previous;
        }

        /// <summary>
        /// Вызов действия в произвольном месте fluent-последовательности, не нарушающего её
        /// </summary>
        /// <typeparam name="T">Тип результата предыдущего действия в последовательности</typeparam>
        /// <param name="previous">Результат предыдущего во fluent-последовательности метода</param>
        /// <param name="action">Действие, которое необходимо выполнить</param>
        /// <returns>Результат выполнения предыдущего метода во fluent-последовательности</returns>
        public static T InvokeAction<T>(this T previous, Action action)
        {
            action?.Invoke();
            return previous;
        }
    }
}