using Strategy.Abstractions;
using Strategy.Abstractions.Commands;
using Zenject;

namespace Strategy.UserControl.Model
{
    public sealed class UIModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCreator>().AsTransient();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCreator>().AsTransient();
            Container.Bind<CommandButtonsModel>().AsSingle();
            Container.Bind<string>().WithId("Chomper").FromInstance("Some name");
            Container.Bind<float>().WithId("Chomper").FromInstance(2.5f);
            Container.Bind<UICenterModel>().AsSingle();
        }
    }
}
