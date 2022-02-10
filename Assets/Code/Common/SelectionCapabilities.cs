using Strategy.Abstractions;
using Strategy.CommonTypes;
using System.Collections.Generic;

namespace Strategy.Common
{
    public sealed class SelectionCapabilities : ISelectionCapabilities
    {
        public bool CanMove => _canMove;
        public bool CanAttack => _canAttack;
        public bool CanBringMatter => _canBringMatter;
        public bool CanPatrol => _canPatrol;
        public List<CommandTypes> Commands => _commands;
        public List<ItemTypes> ProducesUnits => _producesUnits;
        public List<ItemTypes> ProducesBuildings => ProducesBuildings;

        private bool _canMove;
        private bool _canAttack;
        private bool _canBringMatter;
        private bool _canPatrol;
        private List<CommandTypes> _commands;
        private List<ItemTypes> _producesUnits;
        private List<ItemTypes> _producesBuildings;

        public void SetCapabilities(ItemData itemData)
        {
            _canMove = itemData.Commands.Contains(CommandTypes.Move);
            _canAttack = itemData.Commands.Contains(CommandTypes.Attack);
            _canBringMatter = itemData.Commands.Contains(CommandTypes.BringMatter);
            _canPatrol = itemData.Commands.Contains(CommandTypes.Patrol);

            _commands = itemData.Commands;
            if(itemData.Commands.Contains(CommandTypes.ProduceUnit))
                _producesUnits = itemData.Items;
        }
    }
}
