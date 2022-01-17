using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using System;

namespace Strategy.UserControl.Model
{
    public sealed class StopCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] private AssetsContext _assetsContext;

        public override void SpecificCommandCreation(Action<IStopCommand> callback) => 
            callback?.Invoke(_assetsContext.InjectAsset(new StopCommand()));
    }
}
