using CodeBase.Core.UI;

namespace CodeBase.Services.SceneRepository
{
    public interface ISceneRepository
    {
        public void RegisterPlayer(FirstPersonController firstPersonController);
        public void RegisterMainUI(IMainViewPanel mainViewPanel);

        public IMainViewPanel GetMainViewPanel();

        public FirstPersonController GetPlayerController();
    }
}