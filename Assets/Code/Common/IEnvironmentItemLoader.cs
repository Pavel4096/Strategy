using UnityEngine;

namespace Strategy.Common
{
    public interface IEnvironmentItemLoader
    {
        bool TryLoadEnvironmentItem(EnvironmentItem item, Transform parent, bool isGround);
    }
}
