using Game.Scripts.Core.StateMachine;
using Game.Scripts.Infrastructure.Bootstrapper;

namespace Game.Scripts.Infrastructure.GameStateMachine.States
{
    public class LoadingLevelState : State<EGameState>
    {
        private readonly ICoroutineRunnerService _coroutineRunner;

        public LoadingLevelState()
        {
        }
        public override async void OnEnter(ITriggerResponder<EGameState> stateMachine)
        {
            base.OnEnter(stateMachine);
            
            CoroutineRunner.Instance.StopAllCoroutines();
            
            //await UniTask.DelayFrame(1);
            //await _levelBuilderService.CreateCurrentLevel();
            
            stateMachine.FireTrigger(EGameState.Lobby);
        }
    }
}