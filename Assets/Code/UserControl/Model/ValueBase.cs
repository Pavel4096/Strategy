using System;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public class ValueBase<T> : ScriptableObject
    {
        public T Value { get; private set; }

        public event Action<T> ValueChanged;
        
        public void ChangeValue(T value)
        {
            Value = value;
            ValueChanged?.Invoke(value);
        }
    }
}
