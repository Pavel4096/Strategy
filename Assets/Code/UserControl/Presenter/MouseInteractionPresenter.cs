using Strategy.Abstractions;
using Strategy.UserControl.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Strategy.UserControl.Presenter
{
    public sealed class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private EventSystem _eventSystem;
        private Camera _camera;
        private RaycastHit[] _hits;

        private const int _leftMouseButton = 0;
        private const int _defaultArraySize = 5;

        private void Awake()
        {
            _camera = Camera.main;
            _hits = new RaycastHit[_defaultArraySize];
        }

        private void Update()
        {
            if(_eventSystem.IsPointerOverGameObject())
                return;

            if(!Input.GetMouseButtonUp(_leftMouseButton))
                return;
            
            int count = GetHitsCount();
            Array.Sort(_hits, 0, count, new CompareDistances());

            bool selectionFound = false;
            for(var i = 0; i < count; i++)
            {
                ISelectable currentSelectable = _hits[i].collider.GetComponentInParent<ISelectable>();
                if(currentSelectable == null)
                    continue;
                
                _selectedObject.ChangeValue(currentSelectable);
                selectionFound = true;
                break;
            }

            if(!selectionFound)
                _selectedObject.ChangeValue(null);
        }

        private int GetHitsCount()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            int count;
            bool notDone = false;
            do
            {
                count = Physics.RaycastNonAlloc(ray, _hits);
                if(count == _hits.Length)
                {
                    _hits = new RaycastHit[_hits.Length * 2];
                    notDone = true;
                }
            }
            while(notDone);

            return count;
        }

        private sealed class CompareDistances : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y)
            {
                if(x.distance > y.distance)
                    return 1;
                else
                    return -1;
            }
        }
    }
}
