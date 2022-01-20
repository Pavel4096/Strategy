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

        public override void SpecificCommandCreation(Action<IProduceUnitCommand> callback) =>
            callback?.Invoke(_assetsContext.InjectAsset(new ProduceUnitCommand()));
    }
}
