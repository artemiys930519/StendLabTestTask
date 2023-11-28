using System;
using CodeBase.Enums;
using CodeBase.Events;
using CodeBase.Services.SceneRepository;
using CodeBase.Services.StateService;
using CodeBase.Services.StateService.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase
{
    public class SceneComposition : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Transform _monitorPosition;
        [SerializeField] private Transform _firstPersonPosition;

        #endregion

        private SignalBus _signalBus;
        private Camera _playerCamera;
        private Image _crosshair;

        private ISceneRepository _sceneRepository;
        private IStateService _stateService;

        [Inject]
        private void Construct(SignalBus signalBus, ISceneRepository sceneRepository, IStateService stateService)
        {
            _sceneRepository = sceneRepository;
            _signalBus = signalBus;
            _stateService = stateService;
            _signalBus.Subscribe<ChangeStateSignal>(OnChangeView);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ChangeStateSignal>(OnChangeView);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _playerCamera = _sceneRepository.GetPlayer().playerCamera;
            _crosshair = _sceneRepository.GetPlayer().GetComponentInChildren<Image>();
        }

        private void Update()
        {
            if (Input.GetKeyDown("space") &&
                _stateService.GetCurrentState().CurrentView == Enumenators.ViewType.MonitorView)
            {
                _signalBus.Fire(new ChangeStateSignal() {ViewType = Enumenators.ViewType.FirstPersonView});
            }
        }

        private void OnChangeView(ChangeStateSignal changeStateSignal)
        {
            _stateService.SetCurrentState(new State() {CurrentView = changeStateSignal.ViewType});

            if (changeStateSignal.ViewType == Enumenators.ViewType.MonitorView)
            {
                _playerCamera.transform.SetParent(_monitorPosition);
                _playerCamera.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
            else
            {
                _playerCamera.transform.SetParent(_firstPersonPosition);
                _playerCamera.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }

            _crosshair.enabled = changeStateSignal.ViewType == Enumenators.ViewType.FirstPersonView;
            _sceneRepository.GetPlayer().enabled = changeStateSignal.ViewType == Enumenators.ViewType.FirstPersonView;
            Cursor.lockState = changeStateSignal.ViewType == Enumenators.ViewType.FirstPersonView
                ? CursorLockMode.Locked
                : CursorLockMode.None;
        }
    }
}