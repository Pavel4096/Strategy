using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;

namespace Strategy.UserControl.Model
{
    public sealed class BringMatterCommandCreator : CancellableCommandCreator<IBringMatterCommand, IMatterStorage>
    {
        public override IBringMatterCommand CreateCommand(IMatterStorage storage)
        {
            return new BringMatterCommand(storage.GetPosition());
        }
    }
}
