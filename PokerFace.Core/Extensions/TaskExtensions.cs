namespace PokerFace.Core.Extensions
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static ConfiguredTaskAwaitable Capture(this Task task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable<TResult> Capture<TResult>(this Task<TResult> task)
        {
            return task.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable NoCapture(this Task task)
        {
            return task.ConfigureAwait(false);
        }

        public static ConfiguredTaskAwaitable<TResult> NoCapture<TResult>(this Task<TResult> task)
        {
            return task.ConfigureAwait(false);
        }
    }
}