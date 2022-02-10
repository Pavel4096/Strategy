using Strategy.Abstractions;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public interface ICheckFor
    {
        bool Ground(out Vector3 position);
        bool Attackable(out IAttackable attackable);
        bool MatterStorage(out Vector3 position);
        bool GetItem(out GameObject prefab, out Sprite icon, out float productionTime);
        void SetItem(string prefab, Sprite icon, float productionTime);
    }
}
