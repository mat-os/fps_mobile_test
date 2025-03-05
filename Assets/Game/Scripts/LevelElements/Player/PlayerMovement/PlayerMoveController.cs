using Configs;
using Game.Scripts.Infrastructure.Services;
using Game.Scripts.LevelElements.Player.PlayerMovement;
using UnityEngine;
using Zenject;

namespace Game.Scripts.LevelElements.Player
{
    public class PlayerMoveController : IPlayerMoveController
    {
        private readonly PlayerView _playerView;
        private PlayerConfig _playerConfig;
        private readonly CharacterController _characterController;
        private InputService _inputService;

        private Vector3 _verticalVelocity; // Вертикальная скорость игрока
        
        [Inject]
        public void Construct(InputService inputService, GameConfig gameConfig)
        {
            _inputService = inputService;
            _playerConfig = gameConfig.PlayerConfig;
        }
        public PlayerMoveController(PlayerView playerView)
        {
            _playerView = playerView;
            _characterController = _playerView.СharacterController;
        }

        public void HandleMovement(float deltaTime)
        {
            // Получаем ввод от Simple Input System
            float moveX = SimpleInput.GetAxis("Horizontal");
            float moveZ = SimpleInput.GetAxis("Vertical");

            // Рассчитываем направление движения
            Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);

            // Учитываем длину джойстика (интенсивность ввода)
            float inputMagnitude = moveDirection.magnitude; // Длина вектора (от 0 до 1)

            // Преобразуем движение в локальные координаты игрока
            moveDirection = _characterController.transform.TransformDirection(moveDirection);

            // Применяем движение с учётом силы отклонения джойстика
            _characterController.Move(moveDirection * (_playerConfig.MovementConfig.MoveSpeed * inputMagnitude * deltaTime));

            // Обработка гравитации
            if (_characterController.isGrounded)
            {
                _verticalVelocity.y = 0f; // Обнуляем вертикальную скорость, если на земле
            }
            else
            {
                _verticalVelocity.y += _playerConfig.MovementConfig.Gravity * deltaTime; // Применяем гравитацию
            }

            // Применяем вертикальное движение (гравитацию)
            _characterController.Move(_verticalVelocity * deltaTime);
        }
        
        public void HandleLook(float deltaTime)
        {
            Vector2 lookInput = _inputService.LookDelta;

            float rotationX = lookInput.x * _playerConfig.MovementConfig.LookSensitivity;
            float rotationY = lookInput.y * _playerConfig.MovementConfig.LookSensitivity;

            _playerView.transform.Rotate(Vector3.up * rotationX);

            _playerView.Camera.transform.Rotate(Vector3.left * rotationY);
        }
    }
}