using Strategy.Abstractions.Commands;
using UnityEngine;

namespace Strategy.UserControl.Commands
{
    public sealed class MoveCommand : IMoveCommand
    {
        public Vector3 Position { get; }

        public MoveCommand(Vector3 position) => Position = position;
    }
}
