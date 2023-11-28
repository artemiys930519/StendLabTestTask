using System.Collections;
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

        [SerializeField] private float _moveSpeed;
        [SerializeField] private Transform _monitorPosition;
        [SerializeField] private Transform _firstPersonPosition;

        #endregion

        private SignalBus _signalBus;
        private Camera _playerCamera;
        private Image _crosshair;

        private ISceneRepository _sceneRepository;
        private IStateService _stateService;
        private Coroutine _moveCoroutine;

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
            _playerCamera = _sceneRepository.GetPlayerController().playerCamera;
            _crosshair = _sceneRepository.GetPlayerController().GetComponentInChildren<Image>();
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
            }
            else
            {
                _playerCamera.transform.SetParent(_firstPersonPosition);
            }

            _sceneRepository.GetPlayerController().enabled = false;

            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }

            _moveCoroutine =
                StartCoroutine(MoveCoroutine(changeStateSignal.ViewType));

            _crosshair.enabled = changeStateSignal.ViewType == Enumenators.ViewType.FirstPersonView;
            Cursor.lockState = changeStateSignal.ViewType == Enumenators.ViewType.FirstPersonView
                ? CursorLockMode.Locked
                : CursorLockMode.None;
        }

        private IEnumerator MoveCoroutine(Enumenators.ViewType viewType)
        {
            Vector3 destination = Vector3.zero;

            destination = viewType == Enumenators.ViewType.MonitorView
                ? _monitorPosition.transform.position
                : _firstPersonPosition.transform.position;

            while (Vector3.Distance(_playerCamera.transform.position, destination) > 0.001f)
            {
                Vector3 result = Vector3.Lerp(_playerCamera.transform.position, destination,
                    Time.deltaTime * _moveSpeed);
                _playerCamera.transform.position = result;
                yield return new WaitForFixedUpdate();
            }

            _playerCamera.transform.localPosition = Vector3.zero;
            _playerCamera.transform.localRotation = Quaternion.identity;
            _sceneRepository.GetPlayerController().enabled = viewType == Enumenators.ViewType.FirstPersonView;
        }
    }
}