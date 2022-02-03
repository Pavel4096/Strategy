using Strategy.Abstractions.Commands;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy.Core.Commands
{
    public sealed class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        private CancellationTokenSource _cts;

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            NavMeshAgent meshAgent = GetComponent<NavMeshAgent>();
            
            if(meshAgent.enabled)
            {
                meshAgent.SetDestination(gameObject.transform.position);
                meshAgent.enabled = false;
            }
            GetComponent<Animator>().SetTrigger("Idle");

            if(_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        public CancellationToken GetCancellationToken()
        {
            if(_cts == null)
                _cts = new CancellationTokenSource();
            
            return _cts.Token;
        }
    }
}
