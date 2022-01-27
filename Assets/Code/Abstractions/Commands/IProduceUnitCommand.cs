using UnityEngine;

namespace Strategy.Abstractions.Commands
{
    public interface IProduceUnitCommand : ICommand
    {
        GameObject UnitPrefab { get; }
        Sprite Icon { get; }
        string Name { get; }
        float ProductionTime { get; }
    }
}
