using Strategy.CommonTypes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.View
{
    public sealed class NewCommandButtonsView : MonoBehaviour
    {
        public event Action<CommandTypes> CommandSelected;
        public event Action<ItemTypes> ItemSelected;

        [SerializeField] private Button[] _itemButtons;
        [SerializeField] private Image[] _itemImages;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _patrolButton;
        private Dictionary<CommandTypes, Button> _buttonsByCommandType;

        private void Awake()
        {
            _buttonsByCommandType = new Dictionary<CommandTypes, Button>();
            _buttonsByCommandType.Add(CommandTypes.Stop, _stopButton);
            _buttonsByCommandType.Add(CommandTypes.Patrol, _patrolButton);
            ClearButtons();
        }

        public void ClearButtons()
        {
            for(var i = 0; i < _itemButtons.Length; i++)
            {
                _itemButtons[i].onClick.RemoveAllListeners();
                _itemButtons[i].gameObject.SetActive(false);
            }

            foreach(var currentButton in _buttonsByCommandType)
            {
                currentButton.Value.onClick.RemoveAllListeners();
                currentButton.Value.gameObject.SetActive(false);
            }
        }

        public void EnableCommandButton(CommandTypes commandType)
        {
            if(_buttonsByCommandType.ContainsKey(commandType))
            {
                Button currentButton = _buttonsByCommandType[commandType];
                currentButton.gameObject.SetActive(true);
                currentButton.onClick.AddListener(() => CommandSelected?.Invoke(commandType));
            }
        }

        public void EnableItemsButtons(List<ItemTypes> items, List<Sprite> itemImages)
        {
            for(var i = 0; i < items.Count; i++)
            {
                var index = i;
                _itemButtons[i].onClick.AddListener(() => {
                    ItemSelected?.Invoke(items[index]);
                    CommandSelected?.Invoke(CommandTypes.ProduceUnit);
                });
                _itemImages[i].sprite = itemImages[i];
                _itemButtons[i].gameObject.SetActive(true);
            }
        }

        public void UnblockAllButtons()
        {
            foreach(var currentButton in _buttonsByCommandType)
            {
                currentButton.Value.interactable = true;
            }
        }

        public void BlockButton(CommandTypes commandType)
        {
            if(_buttonsByCommandType.ContainsKey(commandType))
                _buttonsByCommandType[commandType].interactable = false;
        }
    }
}
