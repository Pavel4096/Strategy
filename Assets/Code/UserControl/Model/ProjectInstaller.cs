using Strategy.Abstractions;
using Strategy.UserControl.Utils;
using Strategy.UserControl.Utils.AssetsInjector;
using Zenject;
using UnityEngine;

namespace Strategy.UserControl.Model
{
    [CreateAssetMenu(menuName = "Game/Project Installer", fileName = "ProjectInstaller")]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private AssetsContext _assetsContext;
        [SerializeField] private SelectableValue _selectableValue;
        [SerializeField] private Vector3Value _vector3Value;
        [SerializeField] private AttackableValue _attackableValue;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_assetsContext);
            Container.Bind<SelectableValue>().FromInstance(_selectableValue);
            Container.Bind<Vector3Value>().FromInstance(_vector3Value);
            Container.Bind<AttackableValue>().FromInstance(_attackableValue);

            Container.Bind<IAwaitable<ISelectable>>().FromInstance(_selectableValue);
            Container.Bind<IAwaitable<Vector3>>().FromInstance(_vector3Value);
            Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackableValue);
        }
    }
}
