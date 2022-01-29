using System;

namespace Strategy.Abstractions.Commands
{
    public interface ICommandExecutor
    {
        event Action Completed;
        void ExecuteCommand(object command);
    }
}
