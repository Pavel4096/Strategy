using UnityEngine;

namespace Strategy.Abstractions
{
    public interface ISelectable : IContainsIcon
    {
        float MaxHealth { get; }
        float Health { get; }
    }
}
