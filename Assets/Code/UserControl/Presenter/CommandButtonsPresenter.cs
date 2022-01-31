using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Strategy.UserControl.View;
using Strategy.UserControl.Model;
using Strategy.UserControl.Commands;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Strategy.UserControl.Presenter
{
    public sealed class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private CommandButtonsView _commandButtonsView;
        [SerializeField] private AssetsContext _assetsContext;
        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _commandButtonsView.CommandSelected += _model.CommandButtonClicked;
            _model.CommandAccepted += _commandButtonsView.BlockInteraction;
            _model.CommandSent += _commandButtonsView.UnblockAllInteractions;
            _model.CommandSent += _commandButtonsView.EnableDefaultCommand;
            _model.CommandCanceled += _commandButtonsView.UnblockAllInteractions;
            _commandButtonsView.Clear();
            _selectedObject.ValueChanged += SelectionChanged;
            SelectionChanged(_selectedObject.Value);
        }

        private void OnDestroy()
        {
            _commandButtonsView.CommandSelected -= _model.CommandButtonClicked;
            _model.CommandAccepted -= _commandButtonsView.BlockInteraction;
            _model.CommandSent -= _commandButtonsView.UnblockAllInteractions;
            _model.CommandSent -= _commandButtonsView.EnableDefaultCommand;
            _model.CommandCanceled -= _commandButtonsView.UnblockAllInteractions;
            _selectedObject.ValueChanged -= SelectionChanged;
        }

        private void SelectionChanged(ISelectable selectable)
        {
            if(selectable == _currentSelectable)
                return;
            
            if(_currentSelectable != null)
                _model.SelectionChanged();
            
            _commandButtonsView.Clear();
            _currentSelectable = selectable;


            if(selectable != null)
            {
                if(selectable is Component selectableComponent)
                {
                    _model.CommandListExecutor = selectableComponent.GetComponent<ICommandListExecutor>();
                    var commandExecutors = new List<ICommandExecutor>();
                    commandExecutors.AddRange(selectableComponent.GetComponentsInParent<ICommandExecutor>());
                    _commandButtonsView.MakeLayout(commandExecutors);
                }
            }
        }
    }
}
