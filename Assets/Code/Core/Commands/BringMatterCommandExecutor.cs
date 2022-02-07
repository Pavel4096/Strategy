using Strategy.Abstractions.Commands;
using Strategy.Abstractions;
using Strategy.UserControl.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using UnityEngine.AI;

namespace Strategy.Core.Commands
{
    public sealed class BringMatterCommandExecutor : CommandExecutorBase<IBringMatterCommand>, ICanHaveMatter
    {
        public override event Action Completed;

        public float AmountOfMatter => _currentMatterAmount;
        public float MaxAmountOfMatter => _maxMatterAmount;

        [SerializeField] private float _maxMatterAmount;
        [SerializeField] private float _matterPerSecond;
        [SerializeField] private float _maxDistanceToStorage;
        [Inject] INearestBuilding _nearestBuilding;
        [Inject] MatterAmounts _matterAmounts;
        private StopCommandExecutor _stopCommandExecutor;
        private UnitStopper _unitStopper;
        private Animator _animator;
        private NavMeshAgent _meshAgent;
        private int _teamId;
        private float _currentMatterAmount;
        private Vector3 _currentMatterStorageLocation;
        private bool _isCommandActive;
        private bool _isCollectingMatter;

        private void Awake()
        {
            _stopCommandExecutor = gameObject.GetComponent<StopCommandExecutor>();
            _unitStopper = gameObject.GetComponent<UnitStopper>();
            _animator = gameObject.GetComponent<Animator>();
            _meshAgent = gameObject.GetComponent<NavMeshAgent>();
            _teamId = gameObject.GetComponent<ITeam>().TeamID;
        }

        private void Update()
        {
            if(!_isCommandActive)
            {
                _isCollectingMatter = false;
                return;
            }
            
            if((gameObject.transform.position - _currentMatterStorageLocation).sqrMagnitude > _maxDistanceToStorage)
            {
                _isCollectingMatter = false;
                return;
            }
            
            if(_currentMatterAmount < _maxMatterAmount)
            {
                _isCollectingMatter = true;
                _currentMatterAmount += _matterPerSecond * Time.deltaTime;
                if(_currentMatterAmount >= _maxMatterAmount)
                {
                    _currentMatterAmount = _maxMatterAmount;
                    _isCollectingMatter = false;
                }
            }
        }

        public override async void ExecuteSpecificCommand(IBringMatterCommand command)
        {
            _currentMatterStorageLocation = command.MatterStoragePosition;

            try
            {
                while(true)
                {
                    if(_currentMatterAmount >= _maxMatterAmount)
                    {
                        _meshAgent.enabled = true;
                        _meshAgent.SetDestination(_nearestBuilding.GetNearestBuildingPosition(_teamId, gameObject.transform.position));
                        _animator.SetTrigger("Walk");

                        await _unitStopper.WithCancellation(_stopCommandExecutor.GetCancellationToken());
                        
                        _matterAmounts.AddMatter(_teamId, _currentMatterAmount);
                        _currentMatterAmount = 0.0f;
                    }

                    if((gameObject.transform.position - _currentMatterStorageLocation).sqrMagnitude > _maxDistanceToStorage)
                    {
                        _meshAgent.enabled = true;
                        _meshAgent.SetDestination(_currentMatterStorageLocation);
                        _animator.SetTrigger("Walk");

                        await _unitStopper.WithCancellation(_stopCommandExecutor.GetCancellationToken());
                    
                        _meshAgent.enabled = false;
                        _animator.SetTrigger("Idle");
                    }

                    CancellationToken ct = _stopCommandExecutor.GetCancellationToken();

                    while(_isCollectingMatter)
                    {
                        ct.ThrowIfCancellationRequested();
                        Task.Yield();
                    }
                }
            }
            catch {}

            Completed?.Invoke();
        }
    }
}
