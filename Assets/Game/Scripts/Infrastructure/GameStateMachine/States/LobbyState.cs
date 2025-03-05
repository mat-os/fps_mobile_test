using Game.Scripts.Core.StateMachine;
using Game.Scripts.Infrastructure.Services;

namespace Game.Scripts.Infrastructure.GameStateMachine.States
{
    public class LobbyState : State<EGameState>
    {
        private readonly InputService _inputService;

        public LobbyState(
            InputService inputService)
        {
            _inputService = inputService;
        }

        public override void OnEnter(ITriggerResponder<EGameState> stateMachine)
        {
            _inputService.EnableInput(false);
            base.OnEnter(stateMachine);
        }
        
        public override void OnExit()
        {
            base.OnExit();
        }
    }
}