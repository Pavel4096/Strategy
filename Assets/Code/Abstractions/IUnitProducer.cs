using UniRx;

namespace Strategy.Abstractions
{
    public interface IUnitProducer
    {
        IReadOnlyReactiveCollection<IUnitProductionTask> Queue { get; }
        void Cancel(int index);
    }
}
