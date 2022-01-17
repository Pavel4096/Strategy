using Strategy.Abstractions;
using Strategy.Abstractions.Commands;

namespace Strategy.UserControl.Commands
{
    public sealed class AttackCommand : IAttackCommand
    {
        public IAttackable Attackable { get; }

        public AttackCommand(IAttackable attackable) => Attackable = attackable;
    }
}
