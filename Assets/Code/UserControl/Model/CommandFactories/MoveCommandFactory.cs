using Strategy.Abstractions;
using Strategy.CommonTypes;
using Strategy.UserControl.Commands;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class MoveCommandFactory : ICommandFactory
    {
        public bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command)
        {
            commandType = CommandTypes.None;
            command = null;

            if(selectionCapabilities.CanMove)
            {
                if(checkFor.Ground(out Vector3 position))
                {
                    commandType = CommandTypes.Move;
                    command = new MoveCommand(position);

                    return true;
                }
            }

            return false;
        }

        public bool IsOfType(CommandTypes commandType)
        {
            if(commandType == CommandTypes.Move)
                return true;
            else
                return false;
        }
    }
}
