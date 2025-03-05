using UnityEngine;

namespace Game.Scripts.LevelElements.Garage
{
    public class GarageView : MonoBehaviour
    {
        [field:SerializeField] public ShelfView[] Shelves { get; private set; } 
    }
}