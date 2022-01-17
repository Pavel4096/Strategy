using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace Strategy.UserControl.Utils.AssetsInjector
{
    [CreateAssetMenu(fileName = "AssetsContext", menuName = "Game/Assets context")]
    public sealed class AssetsContext : ScriptableObject
    {
        [SerializeField] private Object[] _objects;

        public Object GetObjectOfType(Type type, string name)
        {
            foreach(Object currentObject in _objects)
            {
                if(currentObject.GetType() == type)
                {
                    if(name == null || name.Equals(currentObject.name))
                    {
                        return currentObject;
                    }
                }
            }
            return null;
        }
    }
}
