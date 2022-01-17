using UnityEngine;

namespace Strategy.Abstractions.Commands
{
    public interface IPatrolCommand : ICommand
    {
        Vector3 Position { get; }
    }
}
