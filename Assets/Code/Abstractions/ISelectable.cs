using Strategy.CommonTypes;
using UnityEngine;

namespace Strategy.Abstractions
{
    public interface ISelectable : IContainsIcon
    {
        ItemTypes TypeId { get; }
        float MaxHealth { get; }
        float Health { get; }
    }
}
