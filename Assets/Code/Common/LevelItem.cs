using Strategy.CommonTypes;
using UnityEngine;

namespace Strategy.Common
{
    public abstract class LevelItem : MonoBehaviour
    {
        public ItemTypes Type;
        public string Prefab;
    }
}
