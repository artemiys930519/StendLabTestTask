using CodeBase.Enums;
using CodeBase.Services.StateService.Models;

namespace CodeBase.Services.StateService
{
    public interface IStateService
    {
        public State GetCurrentState();

        public void SetCurrentState(State state);
    }
}