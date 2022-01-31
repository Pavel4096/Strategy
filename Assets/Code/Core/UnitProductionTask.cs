using Strategy.Abstractions;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class UnitProductionTask : IUnitProductionTask
    {
        public string Name { get; }
        public float ProductionTime { get; }
        public float TimeLeft { get; set; }
        public GameObject UnitPrefab { get; }
        public Sprite Icon { get; }

        public UnitProductionTask(string name, float productionTime, GameObject unitPrefab, Sprite icon)
        {
            Name = name;
            ProductionTime = productionTime;
            TimeLeft = productionTime;
            UnitPrefab = unitPrefab;
            Icon = icon;
        }
    }
}
