using CodeBase.Core.UI;

namespace CodeBase.Services.SceneRepository
{
    public class SceneRepository : ISceneRepository
    {
        private FirstPersonController _firstPersonController;
        private IMainViewPanel _mainViewPanel;

        public void RegisterPlayer(FirstPersonController firstPersonController)
        {
            _firstPersonController = firstPersonController;
        }

        public void RegisterMainUI(IMainViewPanel mainViewPanel)
        {
            _mainViewPanel = mainViewPanel;
        }

        public IMainViewPanel GetMainViewPanel()
        {
            return _mainViewPanel;
        }

        public FirstPersonController GetPlayer()
        {
            return _firstPersonController;
        }
    }
}