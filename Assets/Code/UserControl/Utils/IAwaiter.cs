using System.Runtime.CompilerServices;

namespace Strategy.UserControl.Utils
{
    public interface IAwaiter<T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
    }
}
