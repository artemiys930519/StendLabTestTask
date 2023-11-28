using CodeBase.Events;
using CodeBase.Infrastructure.Factory;
using Zenject;
using IFactory = CodeBase.Infrastructure.Factory.IFactory;

namespace CodeBase.Installers
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ActionSignal>();

            Container.Bind<IFactory>().To<Factory>().AsSingle();
        }
    }
}