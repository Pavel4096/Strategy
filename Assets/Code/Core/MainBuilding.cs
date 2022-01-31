using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using System.Threading.Tasks;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class MainBuilding : MonoBehaviour, ISelectable
    {
        public float MaxHealth => _maxHealth;
        public float Health => _health;
        public Sprite Icon => _icon;

        [SerializeField] private float _maxHealth;
        [SerializeField] private Sprite _icon;
        private float _health;

        private void Awake()
        {
            _health = _maxHealth;
        }
    }
}
