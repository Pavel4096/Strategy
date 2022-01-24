using Strategy.Abstractions.Commands;
using Strategy.UserControl.Utils;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using System;
using System.Threading;

namespace Strategy.UserControl.Model
{
    public abstract class CancellableCommandCreator<TCommand, TArgument> : CommandCreatorBase<TCommand> where TCommand : ICommand
    {
        [Inject] private AssetsContext _assetsContext;
        [Inject] private IAwaitable<TArgument> _awaitableArgument;
        private CancellationTokenSource _cts;

        public sealed override async void SpecificCommandCreation(Action<TCommand> callback)
        {
            try
            {
                TArgument value = await _awaitableArgument;
                callback?.Invoke(_assetsContext.InjectAsset(CreateCommand(value)));
            }
            catch(Exception) {}
        }

        public abstract TCommand CreateCommand(TArgument argument);

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            if(_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}
