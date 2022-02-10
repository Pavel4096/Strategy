using Strategy.Abstractions;
using Strategy.CommonTypes;
using Strategy.UserControl.Commands;

namespace Strategy.UserControl.Model
{
    public sealed class StopCommandFactory : ICommandFactory
    {
        public bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command)
        {
            commandType = CommandTypes.Stop;
            command = new StopCommand();

            return true;
        }

        public bool IsOfType(CommandTypes commandType)
        {
            if(commandType == CommandTypes.Stop)
                return true;
            else
                return false;
        }
    }
}
