using System.Numerics;
using my_game.Managers;
using my_game.player;
using my_game.projectiles;
using my_game.UI;
using Raylib_cs;

namespace my_game.state;

public class GameplayState : IGameState
{
    private GameStateManager? _gameStateManager;
    private GameplayUI? _gameplayUI;
    private Random? _random = new Random();
    private Player _player = new Player(new Vector2(400, 240));

    
    public void EnterState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _gameplayUI = new GameplayUI();
        _gameplayUI.InitUI();
        
        GameManager.Instance.audioManager.PlayMusicTrack();
        _player.PlayerDeadEvent += Player_OnPlayerDeadEvent;
    }

    private void Player_OnPlayerDeadEvent()
    {
        Console.WriteLine("Player has died. Transitioning to Game Over state...");
        _gameStateManager?.SetState(new GameOverState());
     
    }

    public void Update()
    {
        var deltaTime = Raylib.GetFrameTime();
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();
        
        var playerPos = _player.Position;
        playerPos  += GameManager.Instance.inputSystem.GetMovement();

        
        playerPos.X = Math.Clamp(playerPos.X, 0, screenWidth);
        playerPos.Y = Math.Clamp(playerPos.Y, 0, screenHeight);
        _player.Position = playerPos;
        
        // Shoot bullets
        if (GameManager.Instance.inputSystem.IsShooting())
        {
 
            var aimDirection = GameManager.Instance.inputSystem.GetAimDirection(_player.Position);
            GameManager.Instance.projectileSystem.AddProjectile(_player.Position, aimDirection, 600f, 1, ProjectileSource.Player);


            _player.Rotation = GameManager.Instance.inputSystem.VectorToAimAngle(aimDirection);

        }
        
        ///////  PROJECTILE SYSTEM UPDATE ///////
        GameManager.Instance.projectileSystem.UpdateProjectiles(deltaTime, GameManager.Instance.enemySystem.GetActiveEnemies());
     
        ///////  COLLISION SYSTEM UPDATE ///////
        GameManager.Instance.collisionSystem.UpdateCollisions(GameManager.Instance.enemySystem.GetActiveEnemies()
            , GameManager.Instance.projectileSystem.GetActiveProjectiles(),_player);
        
        // Spawn enemies randomly
        if (_random?.Next(100) < 2)
        {
            var randomEnemySpawnPosition = new Vector2(_random.Next(0, screenWidth), 5);
            var enemyVelocity = new Vector2(0, 1); // Enemies move downwards
            //var enemySize = new Vector2(170, 102); // Size of the enemy
            GameManager.Instance.enemySystem.AddEnemy(randomEnemySpawnPosition, enemyVelocity , .25f);
        }
        GameManager.Instance.enemySystem.UpdateEnemies(deltaTime);
        
        _gameplayUI?.UpdateUI();
    }

    public void Draw()
    {
        var fps = Raylib.GetFPS();
        Raylib.ClearBackground(Color.Black);
        Raylib.DrawText("FPS:" + fps, 10, 10, 20, Color.White);
        
       // Raylib.DrawCircleV(_player.Position, 10, Color.Blue);
        _player.Draw();
        GameManager.Instance.projectileSystem.DrawProjectiles();
        
        GameManager.Instance.enemySystem.DrawEnemies();
        _gameplayUI?.DrawUI();
    }

    public void ExitState()
    {
        _player.PlayerDeadEvent -= Player_OnPlayerDeadEvent;
    }
}
