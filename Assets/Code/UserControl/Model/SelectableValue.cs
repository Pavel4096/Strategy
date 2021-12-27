using Strategy.Abstractions;
using System;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    [CreateAssetMenu(menuName = "Game/Selectable value", fileName = "SelectableValue")]
    public sealed class SelectableValue : ScriptableObject
    {
        public ISelectable Value { get; private set; }
        public event Action<ISelectable> SelectionChanged;

        public void ChangeValue(ISelectable value)
        {
            Value = value;
            SelectionChanged?.Invoke(value);
        }
    }
}
