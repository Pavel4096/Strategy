using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
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
            var currentItem = new CommandItem(executor, command);
            _commands.Enqueue(currentItem);
        }

        private void CommandCompleted()
        {
            _isInProgress = false;
            _currentItem.Executor.Completed -= CommandCompleted;
            _currentItem = null;
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
