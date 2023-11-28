using CodeBase.Events;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.SceneRepository;
using CodeBase.Services.StateService;
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
            Container.DeclareSignal<ChangeStateSignal>();

            Container.Bind<IFactory>().To<Factory>().AsSingle();
            Container.Bind<ISceneRepository>().To<SceneRepository>().AsSingle();
            Container.Bind<IStateService>().To<StateService>().AsSingle();
        }
    }
}