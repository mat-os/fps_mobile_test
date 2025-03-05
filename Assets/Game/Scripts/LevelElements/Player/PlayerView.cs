using UnityEngine;

namespace Game.Scripts.LevelElements.Player
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public CharacterController СharacterController { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public Transform PickupPoint { get; private set; }

    }
}