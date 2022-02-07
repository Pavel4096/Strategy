using Strategy.Abstractions;
using System;

namespace Strategy.UserControl.Model
{
    public interface IItemSelector
    {
        void Subscribe(Action<ISelectable> handler);
    }
}
