using Strategy.Abstractions;
using System;
using UniRx;
using Zenject;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class UICenterModel
    {
        public ReactiveProperty<IUnitProducer> UnitProducers = new ReactiveProperty<IUnitProducer>();

        [Inject]
        private void Init(SelectableValue selectedObject)
        {
            selectedObject.ValueChanged += UpdateProducer;
        }

        private void UpdateProducer(ISelectable selectable)
        {
            if(selectable is Component selectableComponent)
            {
                UnitProducers.Value = selectableComponent.GetComponent<IUnitProducer>();
            }
            else
                UnitProducers.Value = null;
        }
    }
}
