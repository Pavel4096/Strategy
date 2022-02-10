using Strategy.Abstractions.Commands;
using Strategy.CommonTypes;
using System;

namespace Strategy.Abstractions
{
    public interface ICommandListExecutor
    {
        void AddCommand(ICommandExecutor executor, object command);
        void AddCommand(CommandTypes commandType, object command);
    }
}
