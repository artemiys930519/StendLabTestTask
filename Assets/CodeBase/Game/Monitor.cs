using CodeBase.Enums;
using CodeBase.Events;
using CodeBase.Services.SceneRepository;
using CodeBase.Services.StateService;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.Game
{
    public class Monitor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Inspector

        [SerializeField] private Collider _collider;

        #endregion

        private bool _pointerEnter;
        private ISceneRepository _sceneRepository;
        private IStateService _stateService;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, ISceneRepository sceneRepository, IStateService stateService)
        {
            _signalBus = signalBus;
            _stateService = stateService;
            _sceneRepository = sceneRepository;
        }

        private void Update()
        {
            _collider.enabled = _stateService.GetCurrentState().CurrentView == Enumenators.ViewType.FirstPersonView;
            if (Vector3.Distance(transform.position, _sceneRepository.GetPlayerController().transform.position) > 1.5f ||
                !_pointerEnter)
                return;

            if (_stateService.GetCurrentState().CurrentView == Enumenators.ViewType.MonitorView)
                return;

            _sceneRepository.GetMainViewPanel().ShowPanel();
            _sceneRepository.GetMainViewPanel().SwitchPanel(Enumenators.PanelType.MainPanel);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _pointerEnter = false;
            if (_stateService.GetCurrentState().CurrentView != Enumenators.ViewType.MonitorView)
                _sceneRepository.GetMainViewPanel().HidePanel();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _pointerEnter = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _signalBus.Fire(new ChangeStateSignal() {ViewType = Enumenators.ViewType.MonitorView});
        }
    }
}