using Strategy.Abstractions;
using System;

namespace Strategy.UserControl.Utils
{
    public sealed class Awaiter<T> : IAwaiter<T>
    {
        public bool IsCompleted => _isCompleted;

        private bool _isCompleted;
        private ICompletableValue<T> _completableValue;
        private T _result;
        private Action _continuation;

        public Awaiter(ICompletableValue<T> completableValue)
        {
            _completableValue = completableValue;
            _completableValue.Completed += ProcessCompleted;
        }

        public T GetResult() => _result;

        public void OnCompleted(Action continuation)
        {
            if(_isCompleted)
                continuation?.Invoke();
            else
                _continuation = continuation;
        }

        private void ProcessCompleted(T result)
        {
            _completableValue.Completed -= ProcessCompleted;
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }
    }
}
