using UnityEngine;

namespace Strategy.Abstractions.Commands
{
    public interface IMoveCommand : ICommand
    {
        Vector3 Position { get; }
    }
}
