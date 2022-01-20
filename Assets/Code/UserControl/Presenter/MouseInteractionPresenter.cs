using Strategy.Abstractions;
using Strategy.UserControl.Model;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Strategy.UserControl.Presenter
{
    public sealed class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [Inject] private SelectableValue _selectedObject;
        [Inject] private Vector3Value _vector3Value;
        [Inject] private AttackableValue _attackableValue;

        private Camera _camera;
        private RaycastHit[] _hits;
        private int _groundLayer;

        private const int _leftMouseButton = 0;
        private const int _rightMouseButton = 1;
        private const int _defaultArraySize = 5;
        private const string _groundLayerName = "Ground";

        private void Awake()
        {
            _camera = Camera.main;
            _hits = new RaycastHit[_defaultArraySize];
            _groundLayer = LayerMask.NameToLayer(_groundLayerName);
        }

        private void Update()
        {
            if(_eventSystem.IsPointerOverGameObject())
                return;
        
            if(Input.GetMouseButtonUp(_leftMouseButton))
            {
            
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
            else if(Input.GetMouseButtonUp(_rightMouseButton))
            {
                if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    if(hit.transform.gameObject.layer == _groundLayer)
                        _vector3Value.ChangeValue(hit.point);
                    else
                    {
                        IAttackable attackable = hit.transform.GetComponentInParent<IAttackable>();
                        if(attackable != null)
                            _attackableValue.ChangeValue(attackable);
                    }
                }
            }
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
