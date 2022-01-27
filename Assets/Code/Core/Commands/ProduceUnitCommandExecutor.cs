using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using UniRx;
using Zenject;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        private const int _MAX_QUEUE_SIZE = 6;
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => (IReadOnlyReactiveCollection<IUnitProductionTask>) _queue;

        [SerializeField] private Transform _unitParent;
        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();

        public void Cancel(int index) => RemoveItemAtIndex(index);

        private void Update()
        {
            if(_queue.Count == 0)
                return;
            
            var firstItem = (UnitProductionTask) _queue[0];
            firstItem.TimeLeft -= Time.deltaTime;
            if(firstItem.TimeLeft <= 0)
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

        private void ProduceUnit(UnitProductionTask unitProductionTask)
        {
            var position = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
            Object.Instantiate(unitProductionTask.UnitPrefab, position, Quaternion.identity, _unitParent);
        }

        private void RemoveItemAtIndex(int index)
        {
            for(var i = index; i < _queue.Count - 1; i++)
                _queue[i] = _queue[i + 1];
            
            _queue.RemoveAt(_queue.Count - 1);
        }
    }
}
