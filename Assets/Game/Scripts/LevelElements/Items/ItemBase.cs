using Game.Scripts.Constants;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Items
{
    public abstract class ItemBase : MonoBehaviour, IItem
    {
        [field:SerializeField]public Rigidbody Rigidbody { get; private set; }
        [field:SerializeField]public Collider Collider { get; private set; }
        [field:SerializeField]public Renderer Renderer { get; private set; }

        private int _originalLayer;
        private Transform _originalParent;

        private void Awake()
        {
            _originalLayer = gameObject.layer;
            _originalParent = transform.parent;
        }

        public virtual void Pickup(Transform attachPoint)
        {
            LayerUtils.SetLayerRecursively(gameObject, LayerMask.NameToLayer(LayersConstants.PICKUP_LAYER));

            transform.SetParent(attachPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            Rigidbody.isKinematic = true;
            Collider.enabled = false;
        }

        public virtual void Drop(Vector3 throwForce)
        {
            LayerUtils.SetLayerRecursively(gameObject, _originalLayer);

            transform.SetParent(_originalParent);
            Rigidbody.isKinematic = false;
            Collider.enabled = true;
            
            Rigidbody.AddForce(throwForce, ForceMode.Impulse);
        }

        public void EnableHighlight(float width)
        {
            Renderer.material.SetFloat(ShaderConstants.OUTLINE_WIDTH, width); 
        }

        public void DisableHighlight()
        {
            Renderer.material.SetFloat(ShaderConstants.OUTLINE_WIDTH, 0); 
        }
    }
}