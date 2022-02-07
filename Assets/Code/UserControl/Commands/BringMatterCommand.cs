using Strategy.Abstractions.Commands;
using UnityEngine;

namespace Strategy.UserControl.Commands
{
    public sealed class BringMatterCommand : IBringMatterCommand
    {
        public Vector3 MatterStoragePosition { get; }

        public BringMatterCommand(Vector3 position) => MatterStoragePosition = position;
    }
}
