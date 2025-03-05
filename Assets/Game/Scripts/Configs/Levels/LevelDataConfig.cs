using Game.Scripts.LevelElements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(LevelDataConfig), menuName = "Configs/" + nameof(LevelDataConfig))]
    [InlineEditor]
    public class LevelDataConfig : ScriptableObject
    {
        [field:AssetList(Path = "Game/Prefab/Levels")]
        [field:AssetSelector]
        [field:GUIColor(0,1,0)]
        [field: SerializeField] public LevelView Level { get; private set; }
    }
}