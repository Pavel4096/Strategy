using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Strategy.UserControl.View
{
    public sealed class SelectionView : MonoBehaviour
    {
        [SerializeField] private Renderer[] _renderers;
        [SerializeField] private Material _material;

        public void SetSelected(bool value)
        {
            foreach(var renderer in _renderers)
            {
                var materials = renderer.materials.ToList();

                if(value == true)
                    materials.Add(_material);
                else
                    materials.RemoveAt(materials.Count - 1);
                
                renderer.materials = materials.ToArray();
            }
        }
    }
}
