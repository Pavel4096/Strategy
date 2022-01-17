using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using System;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    public sealed class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _assetsContext;
        private Action<IPatrolCommand> _commandCallback;

        [Inject] private void Init(Vector3Value vector3Value) => vector3Value.ValueChanged += ClickedGround;

        public override void SpecificCommandCreation(Action<IPatrolCommand> callback) =>
            _commandCallback = callback;
        
        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _commandCallback = null;
        }

        private void ClickedGround(Vector3 position)
        {
            _commandCallback?.Invoke(_assetsContext.InjectAsset(new PatrolCommand(position)));
            _commandCallback = null;
        }
    }
}
