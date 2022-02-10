using Strategy.CommonTypes;
using System.Collections.Generic;

namespace Strategy.Abstractions
{
    public interface ISelectionCapabilities
    {
        bool CanMove { get; }
        bool CanAttack { get; }
        bool CanBringMatter { get; }
        bool CanPatrol { get; }
        List<CommandTypes> Commands { get; }
        List<ItemTypes> ProducesUnits { get; }
        List<ItemTypes> ProducesBuildings { get; }
    }
}
