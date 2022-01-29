using System;
using UnityEngine;

namespace Strategy.Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        public virtual event Action Completed;

        public void ExecuteCommand(object command) => ExecuteSpecificCommand((T) command);

        public abstract void ExecuteSpecificCommand(T command);
    }
}
