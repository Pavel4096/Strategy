using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils;
using Zenject;
using System;

namespace Strategy.UserControl.Model
{
    public sealed class CommandButtonsModel
    {
        public event Action<ICommandExecutor> CommandAccepted;
        public event Action CommandSent;
        public event Action CommandCanceled;

        [Inject] private CommandCreatorBase<IStopCommand> _stopCommand;
        [Inject] private CommandCreatorBase<IProduceUnitCommand> _produceUnitCommand;
        [Inject] private CommandCreatorBase<IAttackCommand> _attackCommand;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patrolCommand;
        [Inject] private CommandCreatorBase<IMoveCommand> _moveCommand;

        private bool _isCommandPending;

        public void CommandButtonClicked(ICommandExecutor executor)
        {
            if(_isCommandPending)
                ProcessCancel();
            _isCommandPending = true;

            CommandAccepted?.Invoke(executor);

            _stopCommand.ProcessCommandCreation(executor, (command) => ProcessCommandExecution(executor, command));
            _produceUnitCommand.ProcessCommandCreation(executor, (command) => ProcessCommandExecution(executor, command));
            _attackCommand.ProcessCommandCreation(executor, (command) => ProcessCommandExecution(executor, command));
            _patrolCommand.ProcessCommandCreation(executor, (command) => ProcessCommandExecution(executor, command));
            _moveCommand.ProcessCommandCreation(executor, (command) => ProcessCommandExecution(executor, command));
        }

        public void SelectionChanged()
        {
            _isCommandPending = false;
            ProcessCancel();
        }

        private void ProcessCommandExecution(ICommandExecutor executor, object command)
        {
            executor.ExecuteCommand(command);
            _isCommandPending = false;
            CommandSent?.Invoke();
        }

        private void ProcessCancel()
        {
            _stopCommand.ProcessCancel();
            _produceUnitCommand.ProcessCancel();
            _attackCommand.ProcessCancel();
            _moveCommand.ProcessCancel();

            CommandCanceled?.Invoke();
        }
    }
}
