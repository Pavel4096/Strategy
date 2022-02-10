using Strategy.Abstractions;
using Strategy.CommonTypes;
using Strategy.UserControl.Commands;

namespace Strategy.UserControl.Model
{
    public sealed class AttackCommandFactory : ICommandFactory
    {
        public bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command)
        {
            commandType = CommandTypes.None;
            command = null;

            if(selectionCapabilities.CanAttack)
            {
                if(checkFor.Attackable(out IAttackable attackable))
                {
                    commandType = CommandTypes.Attack;
                    command = new AttackCommand(attackable);

                    return true;
                }
            }

            return false;
        }

        public bool IsOfType(CommandTypes commandType)
        {
            if(commandType == CommandTypes.Attack)
                return true;
            else
                return false;
        }
    }
}
