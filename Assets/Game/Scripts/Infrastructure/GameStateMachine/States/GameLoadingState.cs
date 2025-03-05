using Game.Scripts.Core.StateMachine;

namespace Game.Scripts.Infrastructure.GameStateMachine.States
{
    public class GameLoadingState : State<EGameState>
    {
        public GameLoadingState()
        {
        }

        public override void OnEnter(ITriggerResponder<EGameState> stateMachine)
        {
            base.OnEnter(stateMachine);
            //UniTask.WaitUntil(() => _levelBuilderService.IsReady);
        }

        public override void OnExit()
        {
            
            base.OnExit();
        }
    }
}