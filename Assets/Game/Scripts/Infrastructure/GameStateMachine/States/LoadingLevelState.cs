using Cysharp.Threading.Tasks;
using Game.Scripts.Core.StateMachine;
using Game.Scripts.Infrastructure.Bootstrapper;
using Game.Scripts.Infrastructure.Services;

namespace Game.Scripts.Infrastructure.GameStateMachine.States
{
    public class LoadingLevelState : State<EGameState>
    {
        private readonly ICoroutineRunnerService _coroutineRunner;
        private LevelBuilderService _levelBuilderService;
        private LevelDataService _levelDataService;

        public LoadingLevelState(LevelBuilderService levelBuilderService, LevelDataService levelDataService)
        {
            _levelDataService = levelDataService;
            _levelBuilderService = levelBuilderService;
        }
        public override async void OnEnter(ITriggerResponder<EGameState> stateMachine)
        {
            base.OnEnter(stateMachine);
            
            CoroutineRunner.Instance.StopAllCoroutines();
            
            await UniTask.DelayFrame(1);
            
            _levelDataService.SetCurrentLevelData(0);
            await _levelBuilderService.CreateCurrentLevel();
            
            stateMachine.FireTrigger(EGameState.Lobby);
        }
    }
}