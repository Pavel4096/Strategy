using Strategy.Abstractions;
using Zenject;
using UnityEngine;

namespace Strategy.Common
{
    public sealed class ItemLoader : IItemLoader
    {
        private Transform _unitsParent;
        private DiContainer _diContainer;

        public ItemLoader(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public bool TryLoadItem(SavedLevelItem item, Transform parent, bool isBuilding)
        {
            GameObject newItemPrefab = Resources.Load<GameObject>(item.Prefab);
            Position position = item.Position;
            Vector3 currentPosition = new Vector3(position.x, position.y, position.z);
            Rotation rotation = item.Rotation;
            Quaternion currentRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);

            GameObject newItem = Object.Instantiate(newItemPrefab, currentPosition, currentRotation, parent);
            _diContainer.InjectGameObject(newItem);

            if(isBuilding)
            {
                newItem.GetComponent<IProduceUnitCommandExecutor>().SetUnitParent(_unitsParent);
            }
            newItem.GetComponent<ITeam>().SetTeamID(item.Team);

            return true;
        }

        public void SetUnitsParent(Transform unitsParent) => _unitsParent = unitsParent;
    }
}
