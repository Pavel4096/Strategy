using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using System;
using System.Threading;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _assetsContext;
        private Action<IMoveCommand> _commandCallback;
        private bool doNotNullCallback;

        [Inject] private void Init(Vector3Value vector3Value) => vector3Value.ValueChanged += ClickedGround;

        public override void SpecificCommandCreation(Action<IMoveCommand> callback)
        {
            _commandCallback = callback;
            doNotNullCallback = true;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _commandCallback = null;
        }

        private void ClickedGround(Vector3 position)
        {
            doNotNullCallback = false;
            _commandCallback?.Invoke(_assetsContext.InjectAsset(new MoveCommand(position)));
            if(!doNotNullCallback)
                _commandCallback = null;
        }
    }
}
