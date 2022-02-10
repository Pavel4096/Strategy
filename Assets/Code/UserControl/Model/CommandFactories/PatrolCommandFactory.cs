using Strategy.Abstractions;
using Strategy.CommonTypes;
using Strategy.UserControl.Commands;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class PatrolCommandFactory : ICommandFactory
    {

        public bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command)
        {
            commandType = CommandTypes.None;
            command = null;

            if(selectionCapabilities.CanPatrol)
            {
                if(checkFor.Ground(out Vector3 position))
                {
                    commandType = CommandTypes.Patrol;
                    command = new PatrolCommand(position);

                    return true;
                }
            }

            return false;
        }

        public bool IsOfType(CommandTypes commandType)
        {
            if(commandType == CommandTypes.Patrol)
                return true;
            else
                return false;
        }
    }
}
