using UnityEngine;

namespace Strategy.Common
{
    public interface IItemLoader
    {
        bool TryLoadItem(SavedLevelItem item, Transform parent, bool isBuilding);
        void SetUnitsParent(Transform unitsParent);
    }
}
