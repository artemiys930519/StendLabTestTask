using CodeBase.Enums;
using CodeBase.Events;
using CodeBase.Services.SceneRepository;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public class ApplicationViewPanel : ViewPanel
    {
        #region Inspector

        [Header("Buttons")] 
        
        [SerializeField] private Button _backwordButton;
        [SerializeField] private Button _leftMoveButton;
        [SerializeField] private Button _rightMoveButton;
        [SerializeField] private Button _changeColorButton;
        [SerializeField] private Button _creteObjectButton;

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
            _backwordButton.onClick.AddListener(OnBackButtonClick);
            _leftMoveButton.onClick.AddListener(() => OnAction(Enumenators.ActionType.MoveLeft));
            _rightMoveButton.onClick.AddListener(() => OnAction(Enumenators.ActionType.MoveRight));
            _changeColorButton.onClick.AddListener(() => OnAction(Enumenators.ActionType.ColorChange));
            _creteObjectButton.onClick.AddListener(() => OnAction(Enumenators.ActionType.CreateObject));
        }
        
        private void OnDisable()
        {
            _backwordButton.onClick.RemoveListener(OnBackButtonClick);

            _leftMoveButton.onClick.RemoveListener(() => OnAction(Enumenators.ActionType.MoveLeft));
            _rightMoveButton.onClick.RemoveListener(() => OnAction(Enumenators.ActionType.MoveRight));
            _changeColorButton.onClick.RemoveListener(() => OnAction(Enumenators.ActionType.ColorChange));
            _creteObjectButton.onClick.RemoveListener(() => OnAction(Enumenators.ActionType.CreateObject));
        }

        private void OnAction(Enumenators.ActionType actionType)
        {
            ActionSignal actionSignal = new();
            actionSignal.Action = actionType;

            _signalBus.Fire(actionSignal);
        }

        private void OnBackButtonClick()
        {
            _signalBus.Fire(new ChangeStateSignal() {ViewType = Enumenators.ViewType.FirstPersonView});
            _sceneRepository.GetMainViewPanel().SwitchPanel(Enumenators.PanelType.MainPanel);
        }
    }
}