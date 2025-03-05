using Game.Scripts.Core.StateMachine;

namespace Game.Scripts.Infrastructure.LevelStateMachin.States
{
    public class ExitLevelState : State<ELevelState>
    {
        /*private PlayerService _playerService;

        ExitLevelState(PlayerService playerService)
        {
            _playerService = playerService;
        }*/

        public override void OnEnter(ITriggerResponder<ELevelState> stateMachine)
        {
            //_playerService.Clear();
            base.OnEnter(stateMachine);
        }
    }
}