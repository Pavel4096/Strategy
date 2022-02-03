using Strategy.Abstractions.Commands;
using Strategy.Abstractions;
using Strategy.UserControl.Utils;
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
        private StopCommandExecutor _stopCommandExecutor;
        private bool _isInProgress;

        private void Awake()
        {
            _stopCommandExecutor = GetComponent<StopCommandExecutor>();
        }

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            NavMeshAgent meshAgent = GetComponent<NavMeshAgent>();

            meshAgent.enabled = true;
            meshAgent.SetDestination(command.Position);

            if(!_isInProgress)
            {
                _isInProgress = true;
                GetComponent<Animator>()?.SetTrigger("Walk");
                try
                {
                    await _unitStopper.WithCancellation(_stopCommandExecutor.GetCancellationToken());
                }
                catch {}
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
