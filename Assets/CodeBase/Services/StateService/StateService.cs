using CodeBase.Events;
using CodeBase.Services.StateService.Models;

namespace CodeBase.Services.StateService
{
    public class StateService : IStateService
    {
        private State _currentState = new();

        private void OnViewChange(ChangeStateSignal changeStateSignal)
        {
            _currentState.CurrentView = changeStateSignal.ViewType;
        }

        public State GetCurrentState()
        {
            return _currentState;
        }

        public void SetCurrentState(State state)
        {
            _currentState = state;
        }
    }
}