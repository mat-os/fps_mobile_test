namespace Game.Scripts.LevelElements.Player.PlayerMovement
{
    public interface IPlayerMoveController
    {
        void HandleMovement(float deltaTime);
        void HandleLook(float deltaTime);
    }
}