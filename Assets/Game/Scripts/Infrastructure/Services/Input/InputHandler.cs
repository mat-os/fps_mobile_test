using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Utils
{
    public class InputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public event Action<PointerEventData> OnDown;
        public event Action<PointerEventData> OnUp;
        public event Action<PointerEventData> OnDragEvent; 

        public void OnPointerDown(PointerEventData eventData) => OnDown?.Invoke(eventData);
        public void OnPointerUp(PointerEventData eventData) => OnUp?.Invoke(eventData);
        public void OnDrag(PointerEventData eventData) => OnDragEvent?.Invoke(eventData); 
    }
}