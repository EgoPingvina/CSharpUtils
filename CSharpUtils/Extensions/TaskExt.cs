namespace CSharpUtils.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Расширения для Task-ов
    /// </summary>
    public static class TaskExt
    {
        /// <summary>
        /// Ручное завершение незавершаемой асинхронной операции
        /// </summary>
        /// <typeparam name="T">Возвращаемый результат операции</typeparam>
        /// <param name="task">Асинхронная операция</param>
        /// <param name="cancellationToken">Признак остановки</param>
        /// <exception cref="OperationCanceledException">Была вызвана ручная остановка операции</exception>
        /// <returns>Результат выполнения асинхроннjй операции</returns>
        public static async Task<T> WithCancellation<T>(
            this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(
                s => ((TaskCompletionSource<bool>)s).TrySetResult(true),
                tcs))
                if (task != await Task.WhenAny(task, tcs.Task))
                    throw new OperationCanceledException(cancellationToken);

            return await task;
        }
    }
}