using Game.Scripts.Core.StateMachine;
using Game.Scripts.Infrastructure.LevelStateMachin.States;

namespace Game.Scripts.Infrastructure.LevelStateMachin
{
    public sealed class LevelStateMachine : StateMachineBase<IState<ELevelState>, ELevelState>
    {
        public LevelStateMachine
        (
            PlayLevelState playLevelState,
            CompleteLevelState completeLevelState,
            ExitLevelState exitLevelState
        )
        {
            Initialize();
            
            ConfigureState(playLevelState)
                .Permit(ELevelState.Complete, completeLevelState)
                .Permit(ELevelState.Exit, exitLevelState);

            ConfigureState(completeLevelState)
                .Permit(ELevelState.Exit, exitLevelState);

            ConfigureState(exitLevelState)
                .Permit(ELevelState.Play, playLevelState);
            
            SetState(exitLevelState);
            OnEnterState(CurrentState);
        }
    }
}