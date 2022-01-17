using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Strategy.UserControl.Commands;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using System;

namespace Strategy.UserControl.Model
{
    public sealed class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _assetsContext;
        private Action<IAttackCommand> _commandCallback;

        [Inject] private void Init(AttackableValue attackableValue) =>
            attackableValue.ValueChanged += ClickedAttackable;

        public override void SpecificCommandCreation(Action<IAttackCommand> callback) => _commandCallback = callback;

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            _commandCallback = null;
        }

        private void ClickedAttackable(IAttackable attackable)
        {
            _commandCallback?.Invoke(_assetsContext.InjectAsset(new AttackCommand(attackable)));
            _commandCallback = null;
        }
    }
}
