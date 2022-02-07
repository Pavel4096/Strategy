using UnityEngine;

namespace Strategy.Abstractions
{
    public interface IProduceUnitCommandExecutor
    {
        void SetUnitParent(Transform unitParent);
    }
}
