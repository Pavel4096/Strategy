using Strategy.Abstractions;
using Zenject;

namespace Strategy.Core
{
    public sealed class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeModel>().To<TimeModel>().AsSingle();
        }
    }
}
