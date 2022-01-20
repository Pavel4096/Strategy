using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using System;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _assetsContext;
        private Action<IMoveCommand> _commandCallback;

        [Inject] private void Init(Vector3Value vector3Value) => vector3Value.ValueChanged += ClickedGround;

        public override void SpecificCommandCreation(Action<IMoveCommand> callback) => _commandCallback = callback;

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _commandCallback = null;
        }

        private void ClickedGround(Vector3 position)
        {
            _commandCallback?.Invoke(_assetsContext.InjectAsset(new MoveCommand(position)));
            _commandCallback = null;
        }
    }
}
