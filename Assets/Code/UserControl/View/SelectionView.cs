using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strategy.UserControl.View
{
    public sealed class SelectionView : MonoBehaviour
    {
        [SerializeField] Renderer[] _renderers;
        [SerializeField] Material _material;

        public void SetSelected(bool value)
        {
            foreach(var renderer in _renderers)
            {
                var materials = renderer.materials.ToList();

                if(value == true)
                    materials.Add(_material);
                else
                    materials.Remove(_material);
                
                renderer.materials = materials.ToArray();
            }
        }
    }
}
