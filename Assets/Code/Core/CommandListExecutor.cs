using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.Core.Commands;
using Strategy.CommonTypes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class CommandListExecutor : MonoBehaviour, ICommandListExecutor
    {
        private Queue<CommandItem> _commands = new Queue<CommandItem>();
        private bool _isInProgress;
        private CommandItem _currentItem;

        private void Update()
        {
            if(!_isInProgress && _commands.Count > 0)
            {
                _currentItem = _commands.Dequeue();
                _isInProgress = true;
                _currentItem.Executor.Completed += CommandCompleted;
                _currentItem.Executor.ExecuteCommand(_currentItem.Command);
            }
        }

        public void AddCommand(ICommandExecutor executor, object command)
        {
            if(executor is StopCommandExecutor)
            {
                executor.ExecuteCommand(command);
                _commands.Clear();
            }
            else
            {
                var currentItem = new CommandItem(executor, command);
                _commands.Enqueue(currentItem);
            }
        }

        public void AddCommand(CommandTypes commandType, object command)
        {
            ICommandExecutor executor = GetCommandExecutor(commandType);

            if(executor != null)
                AddCommand(executor, command);
        }

        private void CommandCompleted()
        {
            _isInProgress = false;
            _currentItem.Executor.Completed -= CommandCompleted;
            _currentItem = null;
        }

        private ICommandExecutor GetCommandExecutor(CommandTypes commandType)
        {
            switch(commandType)
            {
                case CommandTypes.Move:
                    return gameObject.GetComponent<MoveCommandExecutor>();
                case CommandTypes.Attack:
                    return gameObject.GetComponent<AttackCommandExecutor>();
                case CommandTypes.BringMatter:
                    return gameObject.GetComponent<BringMatterCommandExecutor>();
                case CommandTypes.Patrol:
                    return gameObject.GetComponent<PatrolCommandExecutor>();
                case CommandTypes.Stop:
                    return gameObject.GetComponent<StopCommandExecutor>();
                case CommandTypes.ProduceUnit:
                    return gameObject.GetComponent<ProduceUnitCommandExecutor>();
                default:
                    return null;
            }
        }

        private class CommandItem
        {
            public readonly ICommandExecutor Executor;
            public readonly object Command;

            public CommandItem(ICommandExecutor executor, object command)
            {
                Executor = executor;
                Command = command;
            }
        }
    }
}
