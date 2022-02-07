using Strategy.Abstractions;

namespace Strategy.UserControl.Model
{
    public interface IItemSelectorWriter
    {
        void ChangeValue(ISelectable newValue);
    }
}
