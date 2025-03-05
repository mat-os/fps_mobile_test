using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = nameof(PrefabRepository), menuName = "Configs/" + nameof(PrefabRepository))]
    public class PrefabRepository : ScriptableObject
    {
        [field: Header("Player")]
        //[field: SerializeField] public PlayerView PlayerView { get; private set; }      
        
        [field: Header("Shelf Items")]
        [field:SerializeField] public ShelfItemData[] ShelfItems{ get; private set; }   
    }
    [System.Serializable]
    public class ShelfItemData
    {
        [field:SerializeField]public GameObject ItemPrefab{ get; private set; }  // Префаб предмета
        [field:SerializeField]public Color[] PossibleColors{ get; private set; }  // Возможные цвета
    }
}