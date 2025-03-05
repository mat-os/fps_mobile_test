using Game.Scripts.Core.StateMachine;
using Game.Scripts.Infrastructure.Services;

namespace Game.Scripts.Infrastructure.GameStateMachine.States
{
    public class LevelState : State<EGameState>
    {
        private InputService _inputService;

        public LevelState(
            InputService inputService)
        {
            _inputService = inputService;
        }

        public override void OnEnter(ITriggerResponder<EGameState> stateMachine)
        {
            _inputService.EnableInput(true);
            base.OnEnter(stateMachine);
        }
        
        public override void OnExit()
        {
            _inputService.EnableInput(false);
            base.OnExit();
        }
    }
}