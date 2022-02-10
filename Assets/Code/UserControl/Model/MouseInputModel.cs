using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.Common;
using Strategy.CommonTypes;
using Zenject;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.Model
{
    public sealed class MouseInputModel
    {
        public event Action ClearView;
        public event Action<CommandTypes> EnableCommandButton;
        public event Action<CommandTypes> BlockButton;
        public event Action UnblockAllButtons;
        public event Action<List<ItemTypes>, List<Sprite>> EnableItemsButtons;

        [Inject] private ConfigData _configData;
        private CommandFactory _commandFactories;
        private ICheckFor _checkFor;
        private ISelectable _currentSelectable;
        private ISelectionCapabilities _currentSelectableCapabilities;
        private ICommandListExecutor _commandListExecutor;
        private List<CommandTypes> _commandTypesWithButtons;
        private bool _isSomeButtonBlocked;

        public MouseInputModel()
        {
            _commandFactories = CommandFactory.GetFactory();
            _commandTypesWithButtons = new List<CommandTypes>();
            _commandTypesWithButtons.Add(CommandTypes.Stop);
            _commandTypesWithButtons.Add(CommandTypes.Patrol);
        }

        public void SetCheckFor(ICheckFor checkFor)
        {
            _checkFor = checkFor;
        }

        public void SelectionChanged(ISelectable selectable)
        {
            if(_currentSelectable == selectable)
                return;

            _currentSelectable = selectable;
            ClearView?.Invoke();
            if(selectable != null)
            {
                ClearCommandFactories();
                _currentSelectableCapabilities = _configData.GetCapabilities(selectable.TypeId);
                _commandListExecutor = (selectable as Component).GetComponent<ICommandListExecutor>();
                for(var i = 0; i < _commandTypesWithButtons.Count; i++)
                {
                    if(_currentSelectableCapabilities.Commands.Contains(_commandTypesWithButtons[i]))
                        EnableCommandButton?.Invoke(_commandTypesWithButtons[i]);
                }
                if(_currentSelectableCapabilities.ProducesUnits != null)
                    EnableItemsButtons?.Invoke(_currentSelectableCapabilities.ProducesUnits, GetItemImages(_currentSelectableCapabilities.ProducesUnits));
            }
        }

        public void ProcessRightMouseClick(ICheckFor checkFor)
        {
            if(_currentSelectable == null)
                return;

            if(_commandFactories.TryCreateCommand(_currentSelectableCapabilities, checkFor, out CommandTypes commandType, out object command))
            {
                _commandListExecutor.AddCommand(commandType, command);
                if(_isSomeButtonBlocked)
                    ClearCommandFactories();
            }
        }

        public void SelectSpecificCommand(CommandTypes commandType)
        {
            UnblockAllButtons?.Invoke();
            BlockButton?.Invoke(commandType);
            _isSomeButtonBlocked = true;
            _commandFactories.SetCommandType(commandType);

            if(commandType == CommandTypes.Stop || commandType == CommandTypes.ProduceUnit)
                ProcessRightMouseClick(_checkFor);
        }

        public void SetItem(ItemTypes itemId)
        {
            string prefab = _configData.GetPrefab(itemId);
            Sprite icon = _configData.GetItemImage(itemId);
            float productionTime = _configData.GetProductionTime(itemId);

            _checkFor.SetItem(prefab, icon, productionTime);
        }

        private void ClearCommandFactories()
        {
            _commandFactories.SetCommandType(CommandTypes.None);
            _isSomeButtonBlocked = false;
            UnblockAllButtons?.Invoke();
        }

        private List<Sprite> GetItemImages(List<ItemTypes> items)
        {
            List<Sprite> images = new List<Sprite>();

            for(var i = 0; i < items.Count; i++)
            {
                Sprite currentImage = _configData.GetItemImage(items[i]);
                images.Add(currentImage);
            }

            return images;
        }
    }
}
