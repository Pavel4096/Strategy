using Strategy.Abstractions;
using Strategy.Common;
using Strategy.UserControl.Model;
using Strategy.UserControl.View;
using Zenject;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Strategy.UserControl.Presenter
{
    public sealed class MouseInputPresenter : MonoBehaviour
    {
        [SerializeField] private NewCommandButtonsView _commandButtonsView;
        [SerializeField] private EventSystem _eventSystem;
        [Inject] private MouseInputModel _mouseInputModel;
        private Camera _camera;
        private int _groundLayer;
        private RaycastHit[] _hits;
        private CheckFor _checkFor;

        private readonly CompareDistances _compareDistances = new CompareDistances();
        private const string _groundLayerName = "Ground";
        private const int _defaultHitsCount = 5;
        private const int _leftMouseButton = 0;
        private const int _rightMouseButton = 1;

        [Inject]
        private void Init()
        {
            _mouseInputModel.ClearView += _commandButtonsView.ClearButtons;
            _mouseInputModel.EnableCommandButton += _commandButtonsView.EnableCommandButton;
            _mouseInputModel.EnableItemsButtons += _commandButtonsView.EnableItemsButtons;
            _mouseInputModel.BlockButton += _commandButtonsView.BlockButton;
            _mouseInputModel.UnblockAllButtons += _commandButtonsView.UnblockAllButtons;
            _commandButtonsView.CommandSelected += _mouseInputModel.SelectSpecificCommand;
            _commandButtonsView.ItemSelected += _mouseInputModel.SetItem;
        }

        private void Awake()
        {
            _camera = Camera.main;
            _groundLayer = LayerMask.NameToLayer(_groundLayerName);
            _hits = new RaycastHit[_defaultHitsCount];
            _checkFor = new CheckFor(_camera, _groundLayer);

            _mouseInputModel.SetCheckFor(_checkFor);
        }

        private void OnDestroy()
        {
            _mouseInputModel.ClearView -= _commandButtonsView.ClearButtons;
            _mouseInputModel.EnableCommandButton -= _commandButtonsView.EnableCommandButton;
            _mouseInputModel.EnableItemsButtons -= _commandButtonsView.EnableItemsButtons;
            _mouseInputModel.BlockButton -= _commandButtonsView.BlockButton;
            _mouseInputModel.UnblockAllButtons -= _commandButtonsView.UnblockAllButtons;
            _commandButtonsView.CommandSelected -= _mouseInputModel.SelectSpecificCommand;
            _commandButtonsView.ItemSelected -= _mouseInputModel.SetItem;
        }

        private void Update()
        {
            if(Input.GetMouseButtonUp(_leftMouseButton))
                ProcessLeftMouseClick();
            else if(Input.GetMouseButtonUp(_rightMouseButton))
                ProcessRightMouseClick();
        }

        private void ProcessLeftMouseClick()
        {
            if(_eventSystem.IsPointerOverGameObject())
                return;
            
            int count = GetHitsCount();
            Array.Sort(_hits, 0, count, _compareDistances);

            ISelectable currentSelectable = null;
            for(var i = 0; i < count; i++)
            {
                currentSelectable = _hits[i].collider.GetComponentInParent<ISelectable>();

                if(currentSelectable != null)
                    break;
            }

            _mouseInputModel.SelectionChanged(currentSelectable);
        }

        private void ProcessRightMouseClick()
        {
            _mouseInputModel.ProcessRightMouseClick(_checkFor);
        }

        private int GetHitsCount()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            bool notDone = false;
            int count;

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
