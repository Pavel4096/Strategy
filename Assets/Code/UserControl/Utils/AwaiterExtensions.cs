using System.Threading;
using System.Threading.Tasks;

namespace Strategy.UserControl.Utils
{
    public static class AwaiterExtensions
    {
        public struct Void
        {}

        public static async Task<T> WithCancellation<T>(this Task<T> originalTask, CancellationToken token)
        {
            var _tcs = new TaskCompletionSource<Void>();

            token.Register(() => _tcs.SetResult(new Void()));

            var newTask = await Task.WhenAny(originalTask, _tcs.Task);
            if(newTask == _tcs.Task)
                token.ThrowIfCancellationRequested();
            
            return await originalTask;
        }

        public static Task<T> WithCancellation<T>(this IAwaitable<T> awaitable, CancellationToken token)
        {
            return WithCancellation(awaitable.AsTask(), token);
        }

        public static Task<T> AsTask<T>(this IAwaitable<T> awaitable)
        {
            return Task.Run(async () => await awaitable);
        }
    }
}
