using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Strategy.UserControl.View;
using Strategy.UserControl.Model;
using Strategy.UserControl.Commands;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy.UserControl.Presenter
{
    public sealed class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedObject;
        [SerializeField] private CommandButtonsView _commandButtonsView;
        [SerializeField] private AssetsContext _assetsContext;
        private ISelectable _currentSelectable;

        private void Start()
        {
            _commandButtonsView.Clear();
            _selectedObject.SelectionChanged += SelectionChanged;
            SelectionChanged(_selectedObject.Value);
            _commandButtonsView.CommandSelected += CommandSelected;
        }

        private void OnDestroy()
        {
            _selectedObject.SelectionChanged -= SelectionChanged;
            _commandButtonsView.CommandSelected -= CommandSelected;
        }

        private void SelectionChanged(ISelectable selectable)
        {
            if(selectable == _currentSelectable)
                return;
            
            _commandButtonsView.Clear();
            _currentSelectable = selectable;

            if(selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _commandButtonsView.MakeLayout(commandExecutors);
            }
        }

        private void CommandSelected(ICommandExecutor commandExecutor)
        {
            switch(commandExecutor)
            {
                case CommandExecutorBase<IProduceUnitCommand> unitProducer:
                    unitProducer.ExecuteSpecificCommand(_assetsContext.InjectAsset(new ProduceUnitCommandDerived()));
                    break;
                case CommandExecutorBase<IAttackCommand> attacker:
                    attacker.ExecuteSpecificCommand(_assetsContext.InjectAsset(new AttackCommand()));
                    break;
                case CommandExecutorBase<IPatrolCommand> patroller:
                    patroller.ExecuteSpecificCommand(_assetsContext.InjectAsset(new PatrolCommand()));
                    break;
                case CommandExecutorBase<IStopCommand> stopper:
                    stopper.ExecuteSpecificCommand(_assetsContext.InjectAsset(new StopCommand()));
                    break;
                case CommandExecutorBase<IMoveCommand> mover:
                    mover.ExecuteSpecificCommand(_assetsContext.InjectAsset(new MoveCommand()));
                    break;
                default:
                    Debug.Log($"{commandExecutor.GetType()} - command not found");
                    break;
            }
        }
    }
}
