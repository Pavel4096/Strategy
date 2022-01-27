using Strategy.Abstractions.Commands;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy.Core.Commands
{
    public sealed class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            NavMeshAgent meshAgent = GetComponent<NavMeshAgent>();
            
            meshAgent.SetDestination(GetComponent<Transform>().position);
            meshAgent.enabled = false;
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
