using System;

namespace Strategy.Abstractions
{
    public interface ITimeModel
    {
        IObservable<int> GameTime { get; }
    }
}
