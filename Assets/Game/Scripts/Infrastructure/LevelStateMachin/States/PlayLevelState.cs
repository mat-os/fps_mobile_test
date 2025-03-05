using Game.Scripts.Core.StateMachine;

namespace Game.Scripts.Infrastructure.LevelStateMachin.States
{
    public class PlayLevelState : State<ELevelState>
    {
        private ITriggerResponder<ELevelState> _stateMachine;
        public PlayLevelState()
        {
        }

        public override void OnEnter(ITriggerResponder<ELevelState> stateMachine)
        {
            base.OnEnter(stateMachine);
            _stateMachine = stateMachine;
            GlobalEventSystem.Broker.Publish(new StartPlayLevelEvent() {  });
        }


        public override void OnExit()
        {
            base.OnExit();
        }
    }
}