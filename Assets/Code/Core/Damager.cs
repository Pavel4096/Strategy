using Strategy.Abstractions;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class Damager : MonoBehaviour, IDamager
    {
        public float Damage => _damage;
        public float AttackDistance => _attackDistance;
        public int AttackDelay => _attackDelay;

        [SerializeField] private float _damage;
        [SerializeField] private float _attackDistance;
        [SerializeField] private int _attackDelay;
    }
}
