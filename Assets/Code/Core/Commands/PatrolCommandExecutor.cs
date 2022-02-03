using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy.Core.Commands
{
    public sealed class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        public override event Action Completed;

        private UnitStopper _unitStopper;
        private StopCommandExecutor _stopCommandExecutor;
        private NavMeshAgent _meshAgent;
        private Animator _animator;
        private bool _isCancelled;
        private TaskCompletionSource<bool> _tcs;

        private void Awake()
        {
            _unitStopper = GetComponent<UnitStopper>();
            _stopCommandExecutor = GetComponent<StopCommandExecutor>();
            _meshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        public override async void ExecuteSpecificCommand(IPatrolCommand command)
        {
            Vector3 endPosition = command.Position;
            Vector3 currentTarget = gameObject.transform.position;

            _meshAgent.enabled = true;
            _animator.SetTrigger("Walk");

            while(!_isCancelled)
            {
                _meshAgent.SetDestination(endPosition);
                _tcs = new TaskCompletionSource<bool>();
                ThreadPool.QueueUserWorkItem(Wait);
                _isCancelled = await _tcs.Task;

                Vector3 temp = currentTarget;
                currentTarget = endPosition;
                endPosition = temp;
            }
            Completed?.Invoke();
        }

        private async void Wait(object obj)
        {
            try
            {
                await _unitStopper.WithCancellation(_stopCommandExecutor.GetCancellationToken());
                _tcs.SetResult(false);
            }
            catch
            {
                _tcs.SetResult(true);
            }
        }
    }
}
