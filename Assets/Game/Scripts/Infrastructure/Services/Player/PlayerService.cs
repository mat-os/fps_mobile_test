/*using System;
using Configs;
using Game.Scripts.Configs;
using Game.Scripts.Configs.Vfx;
using Game.Scripts.Core.Update;
using Game.Scripts.Customization;
using Game.Scripts.Infrastructure.LevelStateMachin;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.Infrastructure.Services.Vfx;
using Game.Scripts.LevelElements.Player;
using PG;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Scripts.Infrastructure.States
{
    public class PlayerService : IDisposable
    {
        public Action<PlayerHumanoid> OnPlayerHumanoidCreated;
        
        private readonly GameConfig _gameConfig;

        private PlayerView _playerView;
        private PlayerHumanoid _playerHumanoid;
        
        [Inject] private DiContainer _container;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [Inject]
        public PlayerService()
        {
            _gameConfig = gameConfig;
        }
        public PlayerView CreatePlayer(Transform playerRoot, 
            Transform playerConstraintTarget,
            bool isFreesPlayerPositionX)
        {
            var playerView = _customizationShopService.GetCurrentPlayerModelConfig().PlayerView;
            _playerView = Object.Instantiate(playerView, playerRoot);
            _playerHumanoid = new PlayerHumanoid(_playerView, _gameConfig.PlayerConfig, playerConstraintTarget);
            _container.Inject(_playerHumanoid);
            OnPlayerHumanoidCreated?.Invoke(_playerHumanoid);
            
            return _playerView;
        }

        private void SubscribeEvents()
        {
        }
        private void UnsubscribeEvents()
        {
        }

        public void Clear()
        {
            _playerView = null;
            if (_playerHumanoid != null)
            {
                _playerHumanoid.Dispose();
                _playerHumanoid = null;
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            
            UnsubscribeEvents();
        }
    }
}*/