using Strategy.Abstractions;
using Strategy.UserControl.Model;
using Strategy.UserControl.View;
using UnityEngine;

namespace Strategy.UserControl.Presenter
{
    public sealed class SelectionPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectedObject;
        private ISelectable _currentSelection;

        private void Start()
        {
            _selectedObject.ValueChanged += UpdateSelection;
        }

        private void OnDestroy()
        {
            _selectedObject.ValueChanged -= UpdateSelection;
        }

        private void UpdateSelection(ISelectable newSelectable)
        {
            if(_currentSelection is Component currentSelection)
            {
                SelectionView currentSelectionView = currentSelection.GetComponentInParent<SelectionView>();
                currentSelectionView.SetSelected(false);
            }

            _currentSelection = newSelectable;

            if(newSelectable is Component newSelection)
            {
                SelectionView newSelectionView = newSelection.GetComponentInParent<SelectionView>();
                newSelectionView.SetSelected(true);
            }
        }
    }
}
