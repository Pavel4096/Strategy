using Zenject;

namespace Strategy.UserControl.NewCommandButtons
{
    public sealed class CommandButtonsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NewCommandButtonsModel>().AsSingle();
        }
    }
}
