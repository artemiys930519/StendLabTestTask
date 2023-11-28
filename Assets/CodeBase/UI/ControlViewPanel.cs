using CodeBase.Enums;
using CodeBase.Events;
using CodeBase.Services.SceneRepository;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public class ControlViewPanel : ViewPanel
    {
        #region Inspector

        [SerializeField] private Button _applicationButton;

        #endregion

        private SignalBus _signalBus;
        private ISceneRepository _sceneRepository;

        [Inject]
        private void Construct(SignalBus signalBus, ISceneRepository sceneRepository)
        {
            _sceneRepository = sceneRepository;
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            HidePanel();
            _applicationButton.onClick.AddListener(OnOpenApplication);
        }

        private void OnDisable()
        {
            _applicationButton.onClick.RemoveListener(OnOpenApplication);
        }

        private void OnOpenApplication()
        {
            _signalBus.Fire(new ChangeStateSignal() {ViewType = Enumenators.ViewType.MonitorView});
            _sceneRepository.GetMainViewPanel().SwitchPanel(Enumenators.PanelType.ApplicationPanel);
        }
    }
}