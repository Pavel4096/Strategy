using Strategy.Abstractions;
using Strategy.CommonTypes;
using Strategy.UserControl.Commands;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class BringMatterCommandFactory : ICommandFactory
    {
        public bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command)
        {
            commandType = CommandTypes.None;
            command = null;

            if(selectionCapabilities.CanBringMatter)
            {
                if(checkFor.MatterStorage(out Vector3 position))
                {
                    commandType = CommandTypes.BringMatter;
                    command = new BringMatterCommand(position);

                    return true;
                }
            }

            return false;
        }

        public bool IsOfType(CommandTypes commandType)
        {
            if(commandType == CommandTypes.BringMatter)
                return true;
            else
                return false;
        }
    }
}
