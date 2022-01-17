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
            foreach(var currentRenderer in _renderers)
            {
                var materials = currentRenderer.sharedMaterials.ToList();

                if(value)
                    materials.Add(_material);
                else
                    materials.Remove(_material);
                
                currentRenderer.sharedMaterials = materials.ToArray();
            }
        }
    }
}
