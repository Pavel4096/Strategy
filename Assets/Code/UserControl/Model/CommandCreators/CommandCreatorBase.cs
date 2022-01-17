using Strategy.Abstractions.Commands;
using System;

namespace Strategy.UserControl.Model
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        public ICommandExecutor ProcessCommandCreation(ICommandExecutor executor, Action<T> callback)
        {
            if(executor is CommandExecutorBase<T>)
                SpecificCommandCreation(callback);
            
            return executor;
        }

        public abstract void SpecificCommandCreation(Action<T> callback);

        public virtual void ProcessCancel() {}
    }
}
