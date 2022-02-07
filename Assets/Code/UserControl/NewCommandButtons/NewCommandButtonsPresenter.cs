using Strategy.UserControl.Model;
using Zenject;
using UniRx;
using UnityEngine;

namespace Strategy.UserControl.NewCommandButtons
{
    public sealed class NewCommandButtonsPresenter : MonoBehaviour
    {
        [Inject] private IItemSelector _itemSelector;

        private void Awake()
        {
            //_itemSelector.Subscribe((selectable) => Debug.Log(selectable));
        }
    }
}
