using Strategy.Abstractions.Commands;
using UnityEngine;

namespace Strategy.Core.Commands
{
    public sealed class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override void ExecuteSpecificCommand(IMoveCommand command)
        {
            Debug.Log("Move");
        }
    }
}
