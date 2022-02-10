using Strategy.Abstractions;
using Strategy.CommonTypes;

namespace Strategy.UserControl.Model
{
    public interface ICommandFactory
    {
        bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command);
        bool IsOfType(CommandTypes commandType);
    }
}
