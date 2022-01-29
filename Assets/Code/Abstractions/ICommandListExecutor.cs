using Strategy.Abstractions.Commands;
using System;

namespace Strategy.Abstractions
{
    public interface ICommandListExecutor
    {
        void AddCommand(ICommandExecutor executor, object command);
    }
}
