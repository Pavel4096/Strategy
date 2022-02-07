using Strategy.Abstractions;
using UniRx;
using System;

namespace Strategy.UserControl.Model
{
    public sealed class ItemSelector : IItemSelectorWriter, IItemSelector
    {
        private readonly Subject<ISelectable> _selector = new Subject<ISelectable>();

        public void ChangeValue(ISelectable newValue)
        {
            _selector.OnNext(newValue);
        }

        public void Subscribe(Action<ISelectable> handler)
        {
            _selector.Subscribe(handler);
        }
    }
}
