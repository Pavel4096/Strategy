using Strategy.Abstractions;
using Strategy.CommonTypes;
using System.Collections.Generic;

namespace Strategy.UserControl.Model
{
    public sealed class CommandFactory
    {
        private List<ICommandFactory> _commandFactories = new List<ICommandFactory>();
        private List<ICommandFactory> _allCommandFactories = new List<ICommandFactory>();
        private ICommandFactory _selectedCommandFactory;

        public bool TryCreateCommand(ISelectionCapabilities selectionCapabilities, ICheckFor checkFor, out CommandTypes commandType, out object command)
        {
            commandType = CommandTypes.None;
            command = null;

            if(_selectedCommandFactory != null)
                _selectedCommandFactory.TryCreateCommand(selectionCapabilities, checkFor, out commandType, out command);
            else
            {
                for(var i = 0; i < _commandFactories.Count; i++)
                {
                    if(_commandFactories[i].TryCreateCommand(selectionCapabilities, checkFor, out commandType, out command))
                        break;
                }
            }

            if(commandType != CommandTypes.None)
                return true;
            else
                return false;
        }

        public static CommandFactory GetFactory()
        {
            var commandFactory = new CommandFactory();
            var moveCommandFactory = new MoveCommandFactory();
            var attackCommandFactory = new AttackCommandFactory();
            var bringMatterCommandFactory = new BringMatterCommandFactory();
            var patrolCommandFactory = new PatrolCommandFactory();
            var stopCommandFactory = new StopCommandFactory();
            var produceUnitCommandFactory = new ProduceUnitCommandFactory();

            commandFactory._commandFactories.Add(bringMatterCommandFactory);
            commandFactory._commandFactories.Add(attackCommandFactory);
            commandFactory._commandFactories.Add(moveCommandFactory);

            commandFactory._allCommandFactories.Add(moveCommandFactory);
            commandFactory._allCommandFactories.Add(bringMatterCommandFactory);
            commandFactory._allCommandFactories.Add(attackCommandFactory);
            commandFactory._allCommandFactories.Add(patrolCommandFactory);
            commandFactory._allCommandFactories.Add(stopCommandFactory);
            commandFactory._allCommandFactories.Add(produceUnitCommandFactory);

            return commandFactory;
        }

        public void SetCommandType(CommandTypes commandType)
        {
            if(commandType == CommandTypes.None)
                _selectedCommandFactory = null;
            else
            {
                for(var i = 0; i < _allCommandFactories.Count; i++)
                {
                    if(_allCommandFactories[i].IsOfType(commandType))
                    {
                        _selectedCommandFactory = _allCommandFactories[i];
                        break;
                    }
                }
            }
        }
    }
}
