using UnityEngine;

namespace Strategy.Common
{
    public sealed class EnvironmentItemLoader : IEnvironmentItemLoader
    {
        public bool TryLoadEnvironmentItem(EnvironmentItem item, Transform parent, bool isGround)
        {
            GameObject newItemPrefab = Resources.Load<GameObject>(item.Prefab);
            Position position = item.Position;
            Vector3 currentPosition = new Vector3(position.x, position.y, position.z);
            Rotation rotation = item.Rotation;
            Quaternion currentRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);

            GameObject newItem = Object.Instantiate(newItemPrefab, currentPosition, currentRotation, parent);

            return true;
        }
    }
}
