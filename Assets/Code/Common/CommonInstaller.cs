using Zenject;

namespace Strategy.Common
{
    public sealed class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ConfigData>().AsSingle();
        }
    }
}
