using DG.Tweening;
using Game.Scripts.Infrastructure.GameStateMachine;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.UI.Screens.Serviсes;
using Game.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Initialization
{
    public class AppCore : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;

        private GameStateMachine _gameStateMachine;
        private InputService _inputService;
        private ScreensService _screensService;

        [Inject]
        public void Construct
        (GameStateMachine gameStateMachine, InputService inputService,
            ScreensService screensService)
        {
            _screensService = screensService;
            _inputService = inputService;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void InitializeGame()
        {
            DOTween.Init();
            DOTween.SetTweensCapacity(500,125);
            
            if (Debug.isDebugBuild)
            {
                FPSMeter.Show = true;
            }
            
            _screensService.OnGameStateEnter(_gameStateMachine.CurrentState);
            _inputService.SetInputHandler(_inputHandler);
        }

        public void EnterGame()
        {
            _gameStateMachine.FireTrigger(EGameState.LevelLoading);
        }

        public static bool IsMobilePlatform
        {
            get
            {
#if UNITY_EDITOR
                return UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android ||
                       UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS;
#else
                return Application.isMobilePlatform;
#endif
            }
        }
    }
}