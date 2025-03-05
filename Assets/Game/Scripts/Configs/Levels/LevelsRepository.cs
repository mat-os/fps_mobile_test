using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(LevelsRepository), menuName = "Configs/" + nameof(LevelsRepository))]
    public class LevelsRepository: ScriptableObject
    {
        [field:ListDrawerSettings(ShowItemCount = true)]
        [field: SerializeField] public List<LevelDataConfig> LevelDataConfigs { get; private set; }
        
        [Space(10)]
        [Title("Debug")]
        public bool IsDebugMode;
        public LevelDataConfig DebugLevelDataConfig;
    }
}