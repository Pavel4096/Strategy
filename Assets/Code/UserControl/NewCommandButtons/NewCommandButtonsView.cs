using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Strategy.UserControl.NewCommandButtons
{
    public sealed class NewCommandButtonsView : MonoBehaviour
    {
        [SerializeField] private Button[] _itemButtons;
        [SerializeField] private Image[] _itemButtonImages;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _produceUnitButton;

        private List<Button> _allButtons;

        private void Awake()
        {
            _allButtons = new List<Button>();
            for(var i = 0; i < _itemButtons.Length; i++)
                _allButtons.Add(_itemButtons[i]);
            
            _allButtons.Add(_moveButton);
            _allButtons.Add(_attackButton);
            _allButtons.Add(_patrolButton);
            _allButtons.Add(_stopButton);
            _allButtons.Add(_produceUnitButton);
        }

        public void Clear()
        {
            for(var i = 0; i < _allButtons.Count; i++)
            {
                _allButtons[i].onClick.RemoveAllListeners();
                _allButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
