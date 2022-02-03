using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zenject;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy.Core.Commands
{
    public sealed class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        public override event Action Completed;

        private Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private Subject<IAttackable> _targetAttackables = new Subject<IAttackable>();
        private IDamager _damager;
        private IAttackable _target;
        private Transform _targetTransform;
        private Vector3 _ourPosition;
        private Quaternion _ourRotation;
        private Vector3 _targetPosition;
        private IHaveHP _ourHP;
        private IHaveHP _targetHP;
        private bool _isCanceled;
        private AttackExecutor _attackExecutor;
        private Animator _animator;
        private NavMeshAgent _meshAgent;
        private StopCommandExecutor _stopCommandExecutor;

        public override async void ExecuteSpecificCommand(IAttackCommand command)
        {
            _damager = gameObject.GetComponent<IDamager>();
            _target = command.Attackable;
            _targetTransform = (_target as Component).GetComponent<Transform>();
            _ourHP = gameObject.GetComponent<IHaveHP>();
            _targetHP = _targetTransform.GetComponent<IHaveHP>();
            Update();
            _attackExecutor = new AttackExecutor(this);

            try
            {
                await _attackExecutor.WithCancellation(_stopCommandExecutor.GetCancellationToken());
            }
            catch {}

            _meshAgent.enabled = false;
            _animator.SetTrigger("Idle");
            _target = null;
            _targetTransform = null;
            Completed?.Invoke();
        }

        private void Awake()
        {
            _targetPositions.Select((position) => new Vector3((float) Math.Round(position.x, 2), (float) Math.Round(position.y, 2), (float) Math.Round(position.z, 2))).Distinct().ObserveOnMainThread().Subscribe(ProcessPosition);
            _targetRotations.ObserveOnMainThread().Subscribe(ProcessRotation);
            _targetAttackables.ObserveOnMainThread().Subscribe(ProcessAttackable);
            _animator = gameObject.GetComponent<Animator>();
            _meshAgent = gameObject.GetComponent<NavMeshAgent>();
            _stopCommandExecutor = gameObject.GetComponent<StopCommandExecutor>();
        }

        private void Update()
        {
            if(_targetTransform == null)
                return;

            lock(this)
            {
                _ourPosition = gameObject.transform.position;
                _ourRotation = gameObject.transform.rotation;
                _targetPosition = _targetTransform.position;
            }
        }

        private void ProcessPosition(Vector3 position)
        {
            _meshAgent.enabled = true;
            _meshAgent.SetDestination(position);
            _animator.SetTrigger("Walk");
        }

        private void ProcessRotation(Quaternion rotation)
        {
            _meshAgent.enabled = false;
            gameObject.transform.rotation = rotation;
        }

        private void ProcessAttackable(IAttackable attackable)
        {
            _animator.SetTrigger("Attack");
            _targetHP.DoDamage(_damager.Damage);
        }

        private sealed class AttackExecutor : IAwaitable<AwaiterExtensions.Void>, ICompletableValue<AwaiterExtensions.Void>
        {
            public event Action<AwaiterExtensions.Void> Completed;
            private AttackCommandExecutor _attackExecutor;
            private float _attackDistance;

            public AttackExecutor(AttackCommandExecutor attackExecutor)
            {
                _attackExecutor = attackExecutor;
                _attackDistance = _attackExecutor._damager.AttackDistance * _attackExecutor._damager.AttackDistance;
                var attackThread = new Thread(PerformAttack);
                attackThread.Start();
            }

            public IAwaiter<AwaiterExtensions.Void> GetAwaiter() => new Awaiter<AwaiterExtensions.Void>(this);

            private void PerformAttack()
            {
                Vector3 ourPosition;
                Quaternion ourRotation;
                Vector3 targetPosition;

                while(true)
                {
                    if(_attackExecutor == null || _attackExecutor._isCanceled ||
                        _attackExecutor._ourHP.HP == 0 || _attackExecutor._targetHP.HP == 0)
                        {
                            Completed?.Invoke(new AwaiterExtensions.Void());
                            return;
                        }

                    lock(_attackExecutor)
                    {
                        ourPosition = _attackExecutor._ourPosition;
                        ourRotation = _attackExecutor._ourRotation;
                        targetPosition = _attackExecutor._targetPosition;
                    }

                    Vector3 direction = targetPosition - ourPosition;

                    if(direction.sqrMagnitude > _attackDistance)
                    {
                        //Vector3 movePosition = targetPosition - direction.normalized * _attackDistance * 0.9f;
                        _attackExecutor._targetPositions.OnNext(targetPosition);
                        Thread.Sleep(100);
                    }
                    else if(ourRotation != Quaternion.LookRotation(direction))
                    {
                        _attackExecutor._targetRotations.OnNext(Quaternion.LookRotation(direction));
                        Thread.Sleep(100);
                    }
                    else
                    {
                        _attackExecutor._targetAttackables.OnNext(_attackExecutor._target);
                        Thread.Sleep(_attackExecutor._damager.AttackDelay);
                    }
                }
            }
        }
    }
}
