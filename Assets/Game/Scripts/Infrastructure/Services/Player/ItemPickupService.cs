using System;
using Configs;
using Game.Scripts.Core.Update;
using Game.Scripts.Core.Update.Interfaces;
using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.States;
using UnityEngine;
using Zenject;
using Game.Scripts.Items;
using Game.Scripts.LevelElements.Player;

namespace Game.Scripts.Services
{
    public class ItemPickupService : IUpdate, IDisposable
    {
        private readonly GameConfig _gameConfig;
        private readonly UpdateService _updateService;
        private readonly PlayerService _playerService;
        
        private PlayerHumanoid _playerHumanoid;
        
        private IItem _currentItem;

        private float PickupDistance => _gameConfig.PlayerConfig.ItemPickupConfig.PickupDistance; 
        private float ThrowForceForward => _gameConfig.PlayerConfig.ItemPickupConfig.ThrowForceForward; 
        private float ThrowForceVertical => _gameConfig.PlayerConfig.ItemPickupConfig.ThrowForceVertical; 

        [Inject]
        public ItemPickupService(GameConfig gameConfig, PlayerService playerService, UpdateService updateService)
        {
            _playerService = playerService;
            _updateService = updateService;
            _gameConfig = gameConfig;
            
            _playerService.OnPlayerHumanoidCreated += PlayerHumanoidCreatedHandle;
            
            _updateService.AddUpdateElement(this);
        }

        private void PlayerHumanoidCreatedHandle(PlayerHumanoid playerHumanoid)
        {
            _playerHumanoid = playerHumanoid;
        }

        public void ManualUpdate(float deltaTime)
        {
            if (IsTapDetected())
            {
                TryPickup();
            }
        }

        private bool IsTapDetected()
        {
            return (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || 
                   Input.GetMouseButtonDown(0);
        }

        private Vector3 GetTapPosition()
        {
            return Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : Input.mousePosition;
        }

        public void TryPickup()
        {
            if (_currentItem != null) 
                return; // Уже держим предмет, не подбираем другой

            Vector3 screenPoint = GetTapPosition();
            Ray ray = _playerHumanoid.Camera.ScreenPointToRay(screenPoint);

            if (Physics.Raycast(ray, out RaycastHit hit, PickupDistance, ~0, QueryTriggerInteraction.Collide))
            {
                IItem item = hit.collider.GetComponent<IItem>();
                if (item != null)
                {
                    _currentItem = item;
                    _currentItem.Pickup(_playerHumanoid.PickupPoint);
                    GlobalEventSystem.Broker.Publish(new PlayerPickupItemEvent());
                }
            }
        }

        public void DropItem()
        {
            if (_currentItem == null) 
                return;

            Vector3 throwDirection = GetDirectionOfThrow();
            _currentItem.Drop(throwDirection);
            _currentItem = null;
        }

        private Vector3 GetDirectionOfThrow()
        {
            return _playerHumanoid.Camera.transform.forward * ThrowForceForward + 
                   _playerHumanoid.Camera.transform.up * ThrowForceVertical;
        }

        public void Dispose()
        {
            _updateService.RemoveUpdateElement(this);
            _playerService.OnPlayerHumanoidCreated -= PlayerHumanoidCreatedHandle;
        }
    }
}
