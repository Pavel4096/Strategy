using Strategy.Abstractions.Commands;
using UnityEngine;

namespace Strategy.Core.Commands
{
    public sealed class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Debug.Log("Patrol");
        }
    }
}
