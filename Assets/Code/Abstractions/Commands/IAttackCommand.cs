namespace Strategy.Abstractions.Commands
{
    public interface IAttackCommand : ICommand
    {
        IAttackable Attackable { get; }
    }
}
