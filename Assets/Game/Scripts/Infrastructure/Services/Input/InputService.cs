using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Scripts.Utils;

namespace Game.Scripts.Infrastructure.Services
{
    public class InputService : IDisposable
    {
        private InputHandler _inputHandler;
        
        private readonly float _screenWidth = Screen.width;
        private readonly float _sensitivity = 0.1f;
        
        public Vector2 LookDelta { get; private set; }

        private Vector2 _startTouchPos;
        
        private bool _isActive;

        public void SetInputHandler(InputHandler inputHandler)
        {
            _inputHandler = inputHandler; 
            _inputHandler.OnDown += OnDown_Handler;
            _inputHandler.OnUp += OnUp_Handler;
            _inputHandler.OnDragEvent += OnDrag_Handler;
        }
        
        public void EnableInput(bool isActive)
        {
            _isActive = isActive;
            _inputHandler.gameObject.SetActive(_isActive);
        }

        private void OnDown_Handler(PointerEventData eventData)
        {
            if (!_isActive) 
                return;

            _startTouchPos = eventData.position; 
        }

        private void OnUp_Handler(PointerEventData eventData)
        {
            if (!_isActive) 
                return;

            LookDelta = Vector2.zero;
        }

        private void OnDrag_Handler(PointerEventData eventData)
        {
            if (!_isActive) 
                return;

            Vector2 touchDelta = eventData.position - _startTouchPos;
            LookDelta = new Vector2(touchDelta.x / _screenWidth, touchDelta.y / _screenWidth) * _sensitivity;

            _startTouchPos = eventData.position;
        }

        public void Dispose()
        {
            if (_inputHandler != null)
            {
                _inputHandler.OnDown -= OnDown_Handler;
                _inputHandler.OnUp -= OnUp_Handler;
                _inputHandler.OnDragEvent -= OnDrag_Handler;
            }
        }
    }
}
