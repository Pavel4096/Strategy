using Strategy.Abstractions;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class MatterStorage : MonoBehaviour, IMatterStorage
    {
        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }
    }
}
