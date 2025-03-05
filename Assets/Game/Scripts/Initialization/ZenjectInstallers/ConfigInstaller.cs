using Configs;
using Game.Scripts.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Initialization.ZenjectInstallers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField, Required]private GameConfig _gameConfig;
        [SerializeField, Required]private UIConfig _uiConfig;
        [SerializeField, Required]private PrefabRepository _prefabRepository;
        [SerializeField, Required]private LevelsRepository _levelsRepository;

        
        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
            Container.Bind<UIConfig>().FromInstance(_uiConfig).AsSingle();
            Container.Bind<PrefabRepository>().FromInstance(_prefabRepository).AsSingle();
            Container.Bind<LevelsRepository>().FromInstance(_levelsRepository).AsSingle();
        }
    }
}