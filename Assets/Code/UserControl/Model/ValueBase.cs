using Strategy.Abstractions;
using Strategy.UserControl.Utils;
using System;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public class ValueBase<T> : ScriptableObject, ICompletableValue<T>, IAwaitable<T>
    {
        public T Value { get; private set; }

        public event Action<T> ValueChanged;

        public event Action<T> Completed;
        
        public void ChangeValue(T value)
        {
            Value = value;
            ValueChanged?.Invoke(value);
            Completed?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new Awaiter<T>(this);
        }
    }
}
