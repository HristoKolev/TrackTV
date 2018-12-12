namespace TrackTv.Data
{
    using System.Threading;
    using System.Threading.Tasks;

    internal static class TaskExtensions
    {
        /// <summary>
        /// Returns a task that will complete when the <see cref="CancellationToken"/> is cancelled.
        /// </summary>
        public static Task AsTask(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<object>();
            cancellationToken.Register(() => tcs.TrySetCanceled(), false);
            return tcs.Task;
        }
    }
}