using UnityEngine;

namespace Strategy.Core
{
    internal interface IUnitProducer
    {
        void ProduceUnit(GameObject prefab);
    }
}
