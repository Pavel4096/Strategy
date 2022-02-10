using Strategy.Abstractions;
using Strategy.CommonTypes;
using Strategy.UserControl.Commands;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class ProduceUnitCommandFactory : ICommandFactory
    {
        public bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command)
        {
            commandType = CommandTypes.None;
            command = null;

            if(checkFor.GetItem(out GameObject prefab, out Sprite icon, out float productionTime))
            {
                commandType = CommandTypes.ProduceUnit;
                command = new ProduceUnitCommand(prefab, icon, productionTime);

                return true;
            }

            return false;
        }

        public bool IsOfType(CommandTypes commandType)
        {
            if(commandType == CommandTypes.ProduceUnit)
                return true;
            else
                return false;
        }
    }
}
