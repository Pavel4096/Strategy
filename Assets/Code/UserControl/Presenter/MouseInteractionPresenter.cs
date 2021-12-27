using Strategy.Abstractions;
using Strategy.UserControl.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Strategy.UserControl.Presenter
{
    public sealed class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private EventSystem _eventSystem;
        private Camera _camera;
        private RaycastHit[] hits;

        private const int _leftMouseButton = 0;

        private void Awake()
        {
            _camera = Camera.main;
            hits = new RaycastHit[5];
        }

        private void Update()
        {
            if(_eventSystem.IsPointerOverGameObject())
                return;

            if(!Input.GetMouseButtonUp(_leftMouseButton))
                return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            int count = Physics.RaycastNonAlloc(ray, hits);

            bool selectionFound = false;
            for(var i = 0; i < count; i++)
            {
                ISelectable currentSelectable = hits[i].collider.GetComponentInParent<ISelectable>();
                if(currentSelectable == null)
                    continue;
                
                _selectedObject.ChangeValue(currentSelectable);
                selectionFound = true;
                break;
            }

            if(!selectionFound)
                _selectedObject.ChangeValue(null);
        }
    }
}
