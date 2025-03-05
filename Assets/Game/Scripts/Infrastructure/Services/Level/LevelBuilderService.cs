using System;
using Configs;
using Cysharp.Threading.Tasks;
using Game.Scripts.Infrastructure.States;
using Game.Scripts.LevelElements;
using Game.Scripts.Utils.Debug;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Infrastructure.Services
{
    public class LevelBuilderService : IDisposable
    {
        public event Action OnLevelCreated;
        public event Action OnLevelRemoved;
        
        private readonly PlayerService _playerService;
        
        private LevelDataConfig _currentLevelDataConfig;
        private LevelView _levelView;
        private LevelDataService _levelDataService;
        private GarageService _garageService;

        public bool IsReady { get; private set; }

        public LevelView LevelView
        {
            get
            {
                if (_levelView != null)
                    return _levelView;

                _levelView = Object.FindObjectOfType<LevelView>();

                if (_levelView == null)
                {
                    CustomDebugLog.LogError($"No level view");
                    return null;
                }

                return _levelView;
            }
        }


        public LevelBuilderService(
            PlayerService playerService, 
            LevelDataService levelDataService,
            GarageService garageService)
        {
            _garageService = garageService;
            _levelDataService = levelDataService;
            _playerService = playerService;
        }
        
        public async UniTask CreateCurrentLevel()
        {
            IsReady = false;
            _currentLevelDataConfig = _levelDataService.GetCurrentLevelData();
            
            _levelView = Object.Instantiate(_currentLevelDataConfig.Level).GetComponent<LevelView>();
            _garageService.Initialize(_levelView);
                
            var playerView = _playerService.CreatePlayer(_levelView.PlayerRoot);
            
            await UniTask.DelayFrame(1);
            GC.Collect();
            DynamicGI.UpdateEnvironment();
            
            OnLevelCreated?.Invoke();
            IsReady = true;
            CustomDebugLog.Log("[LEVEL] Create Current Level");
        }
        
        public void Dispose()
        {
        }
    }
}