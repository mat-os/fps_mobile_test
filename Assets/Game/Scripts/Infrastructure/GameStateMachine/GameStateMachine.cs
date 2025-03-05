using Game.Scripts.Core.StateMachine;
using Game.Scripts.Infrastructure.GameStateMachine.States;

namespace Game.Scripts.Infrastructure.GameStateMachine
{
    public sealed class GameStateMachine : StateMachineBase<IState<EGameState>, EGameState>
    {
        public GameStateMachine
        (
            GameLoadingState gameLoadingState,
            LoadingLevelState loadingLevelState,
            LobbyState lobbyState,
            LevelState levelState)
        {
            Initialize();

            ConfigureState(gameLoadingState)
                .Permit(EGameState.LevelLoading, loadingLevelState);

            ConfigureState(loadingLevelState)
                .Permit(EGameState.Lobby, lobbyState);

            ConfigureState(lobbyState)
                .Permit(EGameState.Level, levelState)
                .Permit(EGameState.LevelLoading, loadingLevelState);
            
            ConfigureState(levelState)
                .Permit(EGameState.LevelLoading, loadingLevelState)
                .Permit(EGameState.Lobby, lobbyState);
            
            SetState(gameLoadingState);
            OnEnterState(CurrentState);
        }
    }
}