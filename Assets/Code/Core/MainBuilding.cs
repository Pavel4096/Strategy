using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class MainBuilding : CommandExecutorBase<IProduceUnitCommand>, ISelectable
    {
        public float MaxHealth => _maxHealth;
        public float Health => _health;
        public Sprite Icon => _icon;

        [SerializeField] private Transform _unitParent;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;
        private float _health;

        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
            Instantiate(command.UnitPrefab, position, Quaternion.identity, _unitParent);
        }

        private void Awake()
        {
            _health = _maxHealth;
        }
    }
}
