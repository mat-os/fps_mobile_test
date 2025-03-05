using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Game.Scripts.Items;
using Configs;
using Game.Scripts.Core.Update;
using Game.Scripts.Core.Update.Interfaces;
using Game.Scripts.Infrastructure.States;
using Game.Scripts.LevelElements.Player;

namespace Game.Scripts.Services
{
    public class ItemSelectService : IUpdate, IDisposable
    {
        private GameConfig _gameConfig;
        private UpdateService _updateService;
        private PlayerService _playerService;
        private PlayerHumanoid _playerHumanoid;

        private float SelectDistance => _gameConfig.PlayerConfig.ItemPickupConfig.PickupDistance;

        private readonly List<IItem> _highlightedItems = new();

        [Inject]
        public ItemSelectService(GameConfig gameConfig, UpdateService updateService, PlayerService playerService)
        {
            _playerService = playerService;
            _updateService = updateService;
            _gameConfig = gameConfig;
            
            _updateService.AddUpdateElement(this);
            _playerService.OnPlayerHumanoidCreated += PlayerHumanoidCreatedHandle;
        }

        private void PlayerHumanoidCreatedHandle(PlayerHumanoid playerHumanoid)
        {
            _playerHumanoid = playerHumanoid;
        }

        public void ManualUpdate(float deltaTime)
        {
            if (_playerHumanoid != null)
                TryHighlightItems();
        }

        private void TryHighlightItems()
        {
            ClearHighlight(); // Очищаем прошлые выделенные предметы

            Collider[] colliders = Physics.OverlapSphere(_playerHumanoid.Camera.transform.position, SelectDistance);
            foreach (Collider collider in colliders)
            {
                IItem item = collider.GetComponent<IItem>();
                if (item != null)
                {
                    item.EnableHighlight(_gameConfig.VisualConfig.ItemOutlineWidth);
                    _highlightedItems.Add(item);
                }
            }
        }

        private void ClearHighlight()
        {
            foreach (var item in _highlightedItems)
            {
                item.DisableHighlight();
            }
            _highlightedItems.Clear();
        }

        public void Dispose()
        {
            _updateService.RemoveUpdateElement(this);
            _playerService.OnPlayerHumanoidCreated -= PlayerHumanoidCreatedHandle;
        }
    }
}
