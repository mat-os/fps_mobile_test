using Game.Scripts.Core.StateMachine;
using Game.Scripts.Infrastructure.Services;

namespace Game.Scripts.Infrastructure.LevelStateMachin.States
{
    public class CompleteLevelState : State<ELevelState>
    {
        private InputService _inputService;

        public CompleteLevelState(
            InputService inputService)
        {
            _inputService = inputService;
        }

        public override void OnEnter(ITriggerResponder<ELevelState> stateMachine)
        {
            base.OnEnter(stateMachine);
            _inputService.EnableInput(false);
        }
        
        public override async void OnExit()
        {
            base.OnExit();
        }
    }
}