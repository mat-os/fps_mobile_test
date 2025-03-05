using Game.Scripts.LevelElements.Garage;
using UnityEngine;

namespace Game.Scripts.LevelElements
{
    public class LevelView : MonoBehaviour
    {
        [field:SerializeField] public Transform PlayerRoot { get; private set; } 
        [field:SerializeField] public GarageView GarageView { get; private set; } 
    }
}