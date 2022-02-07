using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable, IHaveHP
    {
        public float HP => _health;
        public float MaxHealth => _maxHealth;
        public float Health => _health;
        public Sprite Icon => _icon;

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
                Object.Destroy(gameObject);
                _remainingUnits.RemoveUnit(GetComponent<ITeam>(), gameObject.transform);
            }
        }

        private void Awake()
        {
            _health = _maxHealth;
        }
    }
}
