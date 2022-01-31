namespace Strategy.Abstractions
{
    public interface IUnitProductionTask : IContainsIcon
    {
        string Name { get; }
        float TimeLeft { get; }
        float ProductionTime { get; }
    }
}
