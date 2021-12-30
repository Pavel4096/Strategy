using Strategy.Abstractions.Commands;
using UnityEngine;

namespace Strategy.Core.Commands
{
    public sealed class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            Debug.Log("Stop");
        }
    }
}
