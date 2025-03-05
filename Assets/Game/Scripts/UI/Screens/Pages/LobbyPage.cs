using UnityEngine;
using Game.Scripts.Infrastructure.GameStateMachine;
using Game.Scripts.Infrastructure.LevelStateMachin;
using Game.Scripts.UI.Screens.Base.Screens;
using UnityEngine.UI;
using Zenject;

public class LobbyPage : Page
{
    [SerializeField] private Button _playButton;

    private GameStateMachine _gameStateMachine;
    private LevelStateMachine _levelStateMachine;

    [Inject] private DiContainer _container;
    
    [Inject]
    public void Construct
    (
        LevelStateMachine levelStateMachine,
        GameStateMachine gameStateMachine
    )
    {
        _levelStateMachine = levelStateMachine;
        _gameStateMachine = gameStateMachine;
    }

    public override void OnCreate()
    {
        base.OnCreate();
        _playButton.onClick.AddListener(G_StartLevel);
    }
    private void G_StartLevel()
    {
        _levelStateMachine.FireTrigger(ELevelState.Play);
        _gameStateMachine.FireTrigger(EGameState.Level);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        _playButton.onClick.RemoveListener(G_StartLevel);
    }
}
