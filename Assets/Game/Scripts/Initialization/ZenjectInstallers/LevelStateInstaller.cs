using Game.Scripts.Infrastructure.LevelStateMachin;
using Game.Scripts.Infrastructure.LevelStateMachin.States;
using Zenject;

namespace Game.Scripts.Initialization.ZenjectInstallers
{
    public class LevelStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelStateMachine>().AsSingle();

            Container.Bind<PlayLevelState>().AsSingle();
            Container.Bind<CompleteLevelState>().AsSingle();
            Container.Bind<ExitLevelState>().AsSingle();
        }
    }
}