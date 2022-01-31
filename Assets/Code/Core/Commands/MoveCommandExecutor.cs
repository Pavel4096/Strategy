using Strategy.Abstractions.Commands;
using Strategy.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Zenject;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy.Core.Commands
{
    public sealed class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        public override event Action Completed;

        [SerializeField] private UnitStopper _unitStopper;
        private bool _isInProgress;
        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            NavMeshAgent meshAgent = GetComponent<NavMeshAgent>();

            meshAgent.enabled = true;
            meshAgent.SetDestination(command.Position);

            if(!_isInProgress)
            {
                _isInProgress = true;
                GetComponent<Animator>()?.SetTrigger("Walk");
                await _unitStopper;
                GetComponent<Animator>()?.SetTrigger("Idle");
                _isInProgress = false;
                meshAgent.enabled = false;
                Completed?.Invoke();
            }
        }

        private async Task WaitForTargetPoint(NavMeshAgent meshAgent)
        {
            while(meshAgent.pathPending)
            {
                await Task.Yield();
            }

            while(meshAgent.remainingDistance > meshAgent.stoppingDistance)
            {
                await Task.Yield();
            }
        }
    }
}
