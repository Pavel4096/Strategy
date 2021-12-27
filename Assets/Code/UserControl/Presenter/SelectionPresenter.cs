using Strategy.Abstractions;
using Strategy.UserControl.Model;
using Strategy.UserControl.View;
using UnityEngine;

namespace Strategy.UserControl.Presenter
{
    public sealed class SelectionPresenter : MonoBehaviour
    {
        [SerializeField] SelectableValue _selectedObject;
        [SerializeField] ISelectable _currentSelection;

        private void Start()
        {
            _selectedObject.SelectionChanged += UpdateSelection;
        }

        private void OnDestroy()
        {
            _selectedObject.SelectionChanged -= UpdateSelection;
        }

        private void UpdateSelection(ISelectable newSelectable)
        {
            if(_currentSelection != null)
            {
                SelectionView currentSelectionView = (_currentSelection as Component).GetComponentInParent<SelectionView>();
                currentSelectionView.SetSelected(false);
            }

            _currentSelection = newSelectable;

            if(newSelectable != null)
            {
                SelectionView newSelectionView = (newSelectable as Component).GetComponentInParent<SelectionView>();
                newSelectionView.SetSelected(true);
            }
        }
    }
}
