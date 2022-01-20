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
        [SerializeField] Slider _health;
        [SerializeField] TMP_Text _healthText;
        
        private void Start()
        {
            _selectedObject.ValueChanged += UpdateUI;
            UpdateUI(_selectedObject.Value);
        }

        private void OnDestroy()
        {
            _selectedObject.ValueChanged -= UpdateUI;
        }

        private void UpdateUI(ISelectable currentSelectable)
        {
            _icon.enabled = currentSelectable != null;
            _health.gameObject.SetActive(currentSelectable != null);
            _healthText.enabled = currentSelectable != null;

            if(currentSelectable != null)
            {
                _icon.sprite = currentSelectable.Icon;
                _healthText.text = $"{currentSelectable.Health}/{currentSelectable.MaxHealth}";
                _health.minValue = 0;
                _health.maxValue = currentSelectable.MaxHealth;
                _health.value = currentSelectable.Health;
            }
        }
    }
}
