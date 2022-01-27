using Strategy.Abstractions;
using Zenject;
using UniRx;
using System;
using UnityEngine;

namespace Strategy.Core
{
    public sealed class TimeModel : ITimeModel
    {
        public IObservable<int> GameTime => _gameTime.Select(t => (int)t);

        private ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();

        [Inject] private void Init()
        {
            Observable.EveryUpdate().Subscribe((_) => _gameTime.Value += Time.deltaTime);
        }
    }
}
