using Strategy.Abstractions;
using Strategy.CommonTypes;
using Zenject;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class UnitSelectable : MonoBehaviour, ISelectable, IHaveHP
    {
        public ItemTypes TypeId => _typeId;
        public float HP => _health;
        public float MaxHealth => _maxHealth;
        public float Health => _health;
        public Sprite Icon => _icon;

        [SerializeField] private ItemTypes _typeId;
        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;
        [Inject] private RemainingUnits _remainingUnits;
        private float _health;

        public void DoDamage(float damage)
        {
            if(_health < 0)
                return;
            
            _health -= damage;
            if(_health <= 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Die");
                Object.Destroy(gameObject, 1.5f);
                _remainingUnits.RemoveUnit(GetComponent<ITeam>());
            }
        }

        private void Awake()
        {
            _health = _maxHealth;
        }
    }
}
