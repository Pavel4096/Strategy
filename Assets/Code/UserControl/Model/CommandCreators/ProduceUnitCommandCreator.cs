using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using System;

namespace Strategy.UserControl.Model
{
    public sealed class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _assetsContext;
        [Inject] private DiContainer _diContainer;

        public override void SpecificCommandCreation(Action<IProduceUnitCommand> callback)
        {
            ProduceUnitCommand command = _assetsContext.InjectAsset(new ProduceUnitCommand());
            _diContainer.Inject(command);
            callback?.Invoke(command);
        }
    }
}
