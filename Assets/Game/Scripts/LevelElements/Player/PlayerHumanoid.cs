using System;
using Game.Scripts.Core.Update;
using Game.Scripts.Core.Update.Interfaces;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.LevelElements.Player.PlayerMovement;
using UnityEngine;
using Zenject;

namespace Game.Scripts.LevelElements.Player
{
    public class PlayerHumanoid  : IDisposable, IUpdate
    {
        private UpdateService _updateService;
        private InputService _inputService;

        private PlayerView PlayerView { get; set; }
        public Camera Camera => PlayerView.Camera;
        public Transform PickupPoint => PlayerView.PickupPoint;

        private IPlayerMoveController _playerMoveController;

        [Inject] private DiContainer _container;
        
        [Inject]
        private void Construct(UpdateService updateService, InputService inputService)
        {
            _updateService = updateService;
            _inputService = inputService;

            _updateService.AddUpdateElement(this);
        }

        public PlayerHumanoid(PlayerView playerView)
        {
            PlayerView = playerView;
        }

        public void Initialize()
        {
            _playerMoveController = new PlayerMoveController(PlayerView);
            _container.Inject(_playerMoveController);
        }
        public void ManualUpdate(float deltaTime)
        {
            _playerMoveController.HandleMovement(deltaTime);
            _playerMoveController.HandleLook(deltaTime);
        }
        public void Dispose()
        {
            _updateService.RemoveUpdateElement(this);
        }
    }
}