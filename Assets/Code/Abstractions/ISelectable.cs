using UnityEngine;

namespace Strategy.Abstractions
{
    public interface ISelectable
    {
        float MaxHealth { get; }
        float Health { get; }
        Sprite Icon { get; }
    }
}
