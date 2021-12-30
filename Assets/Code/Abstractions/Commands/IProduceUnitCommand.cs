using UnityEngine;

namespace Strategy.Abstractions.Commands
{
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
    }
}
