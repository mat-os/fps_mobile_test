using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Configs/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        [field: Header("Player Config")]
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        
        [field: Header("Items Config")]
        [field: SerializeField] public ShelfConfig ShelfConfig { get; private set; }
        
        [field: Header("Visual Config")]
        [field: SerializeField] public VisualConfig VisualConfig { get; private set; }
    }

    [Serializable]
    public class ShelfConfig
    {
        [field: SerializeField] public int ItemsInShelfCount { get; private set; } = 5;
    }    
    [Serializable]
    public class VisualConfig
    {
        [field: SerializeField] public float ItemOutlineWidth { get; private set; } = 0.02f;
    }
}