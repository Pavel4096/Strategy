using Strategy.Abstractions;
using Strategy.UserControl.Model;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Stragety.UserControl.Presenter
{
    public sealed class LeftUIPresenter : MonoBehaviour
    {
        [SerializeField] SelectableValue _selectedObject;
        [SerializeField] Image _icon;
        [SerializeField] TMP_Text _healthText;
        
        private void Start()
        {
            _selectedObject.SelectionChanged += UpdateUI;
            UpdateUI(_selectedObject.Value);
        }

        private void OnDestroy()
        {
            _selectedObject.SelectionChanged -= UpdateUI;
        }

        private void UpdateUI(ISelectable currentSelectable)
        {
            _icon.enabled = currentSelectable != null;
            _healthText.enabled = currentSelectable != null;

            if(currentSelectable != null)
            {
                _icon.sprite = currentSelectable.Icon;
                _healthText.text = $"{currentSelectable.Health}/{currentSelectable.MaxHealth}";
            }
        }
    }
}
