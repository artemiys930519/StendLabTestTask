using CodeBase.Enums;
using CodeBase.Events;
using CodeBase.Infrastructure.Command;
using UnityEngine;
using Zenject;
using IFactory = CodeBase.Infrastructure.Factory.IFactory;

namespace CodeBase.Managers
{
    public class CommandController : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private MeshFilter _spawnRegion;
        [SerializeField] private GameObject _cube;
        [SerializeField] private GameObject _ObjectPrefab;

        #endregion

        private SignalBus _signalBus;

        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;

        private ICommand _previousCommand;
        private Enumenators.ActionType _previousActionType;
        private IFactory _factory;

        [Inject]
        private void Construct(SignalBus signalBus, IFactory factory)
        {
            _signalBus = signalBus;
            _factory = factory;
            signalBus.Subscribe<ActionSignal>(OnGetAction);
        }

        private void OnEnable()
        {
            if (_cube.TryGetComponent(out Rigidbody rigidbody))
            {
                _rigidbody = rigidbody;
            }

            if (_cube.TryGetComponent(out MeshRenderer meshRenderer))
            {
                _meshRenderer = meshRenderer;
            }
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ActionSignal>(OnGetAction);
        }

        private void OnGetAction(ActionSignal actionSignal)
        {
            if (!Validate())
                return;

            if (_previousCommand is IUndoCommand undoCommand)
            {
                undoCommand?.Undo();
                if (actionSignal.Action == _previousActionType)
                {
                    _previousCommand = null;
                    return;
                }
            }

            switch (actionSignal.Action)
            {
                case Enumenators.ActionType.MoveLeft:
                    _previousCommand = new LeftMoveCommand(_rigidbody);
                    break;

                case Enumenators.ActionType.MoveRight:
                    _previousCommand = new RightMoveCommand(_rigidbody);
                    break;

                case Enumenators.ActionType.ColorChange:
                    _previousCommand = new ColorChangeCommand(_meshRenderer);
                    break;

                case Enumenators.ActionType.CreateObject:
                    _previousCommand = new ObjectCreateCommand(_ObjectPrefab, _factory, _spawnRegion);
                    break;
            }

            _previousActionType = actionSignal.Action;
            _previousCommand.Execute();
        }

        private bool Validate()
        {
            return _rigidbody != null || _meshRenderer != null;
        }
    }
}