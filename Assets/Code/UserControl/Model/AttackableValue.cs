using Strategy.Abstractions;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    [CreateAssetMenu(menuName = "Game/Attackable value", fileName = "AttackableValue")]
    public sealed class AttackableValue : ValueBase<IAttackable>
    {}
}
