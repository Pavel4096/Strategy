using Strategy.Abstractions.Commands;
using UnityEngine;

namespace Strategy.Core.Commands
{
    public sealed class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log("Attack");
        }
    }
}
