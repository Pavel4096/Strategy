using UnityEngine;

namespace Strategy.Abstractions.Commands
{
    public interface IBringMatterCommand : ICommand
    {
        Vector3 MatterStoragePosition { get; }
    }
}
