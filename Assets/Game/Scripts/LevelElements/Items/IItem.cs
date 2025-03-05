using UnityEngine;

namespace Game.Scripts.Items
{
    public interface IItem
    {
        Rigidbody Rigidbody { get; }
        Collider Collider { get; }
        void Pickup(Transform attachPoint);
        void Drop(Vector3 throwForce);
        void EnableHighlight(float width);
        void DisableHighlight();
    }
}