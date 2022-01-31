using Strategy.Abstractions;
using Strategy.UserControl.Model;
using Strategy.UserControl.View;
using System;
using UniRx;
using Zenject;
using UnityEngine;

namespace Strategy.UserControl.Presenter
{
    public sealed class UICenterPresenter : MonoBehaviour
    {
        [SerializeField] GameObject _centerUIObject;
        private IDisposable _productionQueueAdd;
        private IDisposable _productionQueueRemove;
        private IDisposable _productionQueueReplace;
        private IDisposable _buttonClicks;

        [Inject]
        private void Init(UICenterModel model, UICenterView view)
        {
            model.UnitProducers.Subscribe((IUnitProducer producer) => {
                _productionQueueAdd?.Dispose();
                _productionQueueRemove?.Dispose();
                _productionQueueReplace?.Dispose();
                _buttonClicks?.Dispose();

                view.Clear();
                _centerUIObject.SetActive(producer != null);

                if(producer != null)
                {
                    _productionQueueAdd = producer.Queue.ObserveAdd().Subscribe(item =>
                        view.SetItem(item.Value, item.Index)
                    );

                    _productionQueueRemove = producer.Queue.ObserveRemove().Subscribe(item =>
                        view.SetItem(null, item.Index)
                    );

                    _productionQueueReplace = producer.Queue.ObserveReplace().Subscribe(item =>
                        view.SetItem(item.NewValue, item.Index)
                    );

                    _buttonClicks = view.ButtonClicks.Subscribe(producer.Cancel);

                    for(var i = 0; i < producer.Queue.Count; i++)
                        view.SetItem(producer.Queue[i], i);
                }
            });
        }
    }
}
