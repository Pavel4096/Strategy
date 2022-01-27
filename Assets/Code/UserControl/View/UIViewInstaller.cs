using Zenject;

namespace Strategy.UserControl.View
{
    public sealed class UIViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UICenterView>().FromComponentInHierarchy().AsSingle();
        }
    }
}
