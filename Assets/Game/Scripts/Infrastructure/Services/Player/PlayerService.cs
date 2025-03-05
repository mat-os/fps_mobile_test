using System;
using Configs;
using Game.Scripts.Configs;
using Game.Scripts.LevelElements.Player;
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
        private PrefabRepository _prefabRepository;

        [Inject]
        public PlayerService(GameConfig gameConfig, PrefabRepository prefabRepository)
        {
            _prefabRepository = prefabRepository;
            _gameConfig = gameConfig;
        }
        public PlayerView CreatePlayer(Transform playerRoot)
        {
            _playerView = Object.Instantiate(_prefabRepository.PlayerView, playerRoot);
            _playerView.transform.SetPositionAndRotation(playerRoot.transform.position, playerRoot.transform.rotation);
            _playerHumanoid = new PlayerHumanoid(_playerView);
            _container.Inject(_playerHumanoid);
            _playerHumanoid.Initialize();
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
}