using System;

namespace Strategy.Abstractions
{
    public interface ICompletableValue<T>
    {
        event Action<T> Completed;
    }
}
