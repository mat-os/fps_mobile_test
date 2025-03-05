using Configs;
using Game.Scripts.Utils.Debug;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Services
{
    public class LevelDataService
    {
        private readonly LevelsRepository _levelsRepository;

        private LevelDataConfig _levelDataConfig;
        
        public bool IsStartDebugLevel => 
            _levelsRepository.IsDebugMode && Application.isEditor;
        
        public LevelDataService(LevelsRepository levelsRepository)
        {
            _levelsRepository = levelsRepository;
        }
        public void SetCurrentLevelData(int levelId)
        {
            // Используем остаток от деления для циклического перехода
            int levelCount = _levelsRepository.LevelDataConfigs.Count;

            if (levelCount == 0)
            {
                CustomDebugLog.LogError("No levels available in LevelDataConfigs!");
                return;
            }

            // Обеспечиваем, что индекс будет циклическим
            int adjustedLevelId = levelId % levelCount;
            
            // Устанавливаем данные уровня на основе скорректированного индекса
            _levelDataConfig = _levelsRepository.LevelDataConfigs[adjustedLevelId];

            CustomDebugLog.Log($"Level set to index: {levelId} (requested: {levelId})");
        }
        
        public LevelDataConfig GetCurrentLevelData()
        {
#if UNITY_EDITOR
            if (IsStartDebugLevel)
            {
                CustomDebugLog.Log("Playing Debug level data ");
                return _levelsRepository.DebugLevelDataConfig;
            }
#endif
            
            if (_levelDataConfig != null)
            {
                CustomDebugLog.Log("Loading level data " + _levelDataConfig);
                return _levelDataConfig;
            }
            else
            {
                CustomDebugLog.LogWarning("NOT FOUND level data!!!");
                return _levelsRepository.LevelDataConfigs[0];
            }
        }
    }
}