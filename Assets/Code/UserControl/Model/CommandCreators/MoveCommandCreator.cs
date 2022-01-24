using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class MoveCommandCreator : CancellableCommandCreator<IMoveCommand, Vector3>
    {
        public override IMoveCommand CreateCommand(Vector3 position)
        {
            return new MoveCommand(position);
        }
    }
}
