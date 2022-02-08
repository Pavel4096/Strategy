using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using UniRx;
using Zenject;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IProduceUnitCommandExecutor, IUnitProducer
    {
        private const int _MAX_QUEUE_SIZE = 6;
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => (IReadOnlyReactiveCollection<IUnitProductionTask>) _queue;

        [SerializeField] private Transform _unitParent;
        //[SerializeField] private Transform _point;
        [Inject] private RemainingUnits _remainingUnits;
        [Inject] private MatterAmounts _matterAmounts;
        [Inject] private DiContainer _diContainer;
        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
        private Vector3 _point;
        private int _teamId;
        private const float _matterPerUnit = 150;

        public void Cancel(int index) => RemoveItemAtIndex(index);

        private void Update()
        {
            if(_queue.Count == 0)
                return;
            
             if(_teamId == 0)
                _teamId = gameObject.GetComponentInChildren<Team>().TeamID;
            
            var firstItem = (UnitProductionTask) _queue[0];
            firstItem.TimeLeft -= Time.deltaTime;
            if(firstItem.TimeLeft <= 0 && _matterAmounts.RemoveMatter(_teamId, _matterPerUnit))
            {
                ProduceUnit(firstItem);
                RemoveItemAtIndex(0);
            }
        }
        
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            if(_queue.Count == _MAX_QUEUE_SIZE)
                return;

            var newUnitProductionTask = new UnitProductionTask(command.Name, command.ProductionTime, command.UnitPrefab, command.Icon);
            _queue.Add(newUnitProductionTask);
        }

        public void SetUnitParent(Transform unitParent) => _unitParent = unitParent;

        private void ProduceUnit(UnitProductionTask unitProductionTask)
        {
            var position = gameObject.transform.position;
            var newUnit = Object.Instantiate(unitProductionTask.UnitPrefab, position, Quaternion.identity, _unitParent);
            var newUnitTeam = newUnit.GetComponentInChildren<Team>();

            newUnitTeam.SetTeamID(_teamId);
            _diContainer.InjectGameObject(newUnit);

            CommandExecutorBase<IMoveCommand> executor = newUnit.GetComponentInChildren<CommandExecutorBase<IMoveCommand>>();
            executor.ExecuteCommand(new MoveCommand(_point));
            _remainingUnits.AddUnit(newUnitTeam);
        }

        private void RemoveItemAtIndex(int index)
        {
            for(var i = index; i < _queue.Count - 1; i++)
                _queue[i] = _queue[i + 1];
            
            _queue.RemoveAt(_queue.Count - 1);
        }

        private sealed class MoveCommand : IMoveCommand
        {
            public Vector3 Position { get; }

            public MoveCommand(Vector3 position)
            {
                Position = position;
            }
        }
    }
}
