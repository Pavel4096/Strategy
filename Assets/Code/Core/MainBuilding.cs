using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable
    {
        private const int _UNIT_PRODUCTION_DELAY = 1000;

        public float MaxHealth => _maxHealth;
        public float Health => _health;
        public Sprite Icon => _icon;

        [SerializeField] private Transform _unitParent;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;
        private float _health;

        private IUnitProducer _unitProducer;

        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            _unitProducer.ProduceUnit(command.UnitPrefab);
        }

        private void Awake()
        {
            _health = _maxHealth;
            _unitProducer = new UnitProducer(_unitParent, _UNIT_PRODUCTION_DELAY);
        }
    }
}
