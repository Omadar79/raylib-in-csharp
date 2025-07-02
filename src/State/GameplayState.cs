using System.Numerics;
using my_game.Managers;
using my_game.player;
using my_game.projectiles;
using Raylib_cs;

namespace my_game.state;

public class GameplayState : IGameState
{

    private Random _random = new Random();
    private Player _player = new Player(new Vector2(400, 240));

    private GameStateManager _gameStateManager;
    
    public void EnterState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _player.PlayerDeadEvent += Player_OnPlayerDeadEvent;
    }

    private void Player_OnPlayerDeadEvent()
    {
        Console.WriteLine("Player has died. Transitioning to Game Over state...");
        _gameStateManager.SetState(new GameOverState());
     
    }

    public void Update()
    {
        var deltaTime = Raylib.GetFrameTime();
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        var playerPos = _player.Position;
        playerPos  += GameManager.Instance.InputSystem.GetMovement();

        
        playerPos.X = Math.Clamp(playerPos.X, 0, screenWidth);
        playerPos.Y = Math.Clamp(playerPos.Y, 0, screenHeight);
        _player.Position = playerPos;
        
        // Shoot bullets
        if (GameManager.Instance.InputSystem.IsShooting())
        {
 
            var aimDirection = GameManager.Instance.InputSystem.GetAimDirection(_player.Position);
            GameManager.Instance.ProjectileSystem.AddProjectile(_player.Position, aimDirection, 600f, 1, ProjectileSource.Player);


            _player.Rotation = GameManager.Instance.InputSystem.VectorToAimAngle(aimDirection);

        }
        
        ///////  PROJECTILE SYSTEM UPDATE ///////
        GameManager.Instance.ProjectileSystem.UpdateProjectiles(deltaTime, GameManager.Instance.EnemySystem.GetActiveEnemies());
     
        ///////  COLLISION SYSTEM UPDATE ///////
        GameManager.Instance.CollisionSystem.UpdateCollisions(GameManager.Instance.EnemySystem.GetActiveEnemies()
            , GameManager.Instance.ProjectileSystem.GetActiveProjectiles(),_player);
        
        // Spawn enemies randomly
        if (_random.Next(100) < 2)
        {
            var randomEnemySpawnPosition = new Vector2(_random.Next(0, screenWidth), 5);
            var enemyVelocity = new Vector2(0, 1); // Enemies move downwards
            //var enemySize = new Vector2(170, 102); // Size of the enemy
            GameManager.Instance.EnemySystem.AddEnemy(randomEnemySpawnPosition, enemyVelocity , .25f);
        }
        GameManager.Instance.EnemySystem.UpdateEnemies(deltaTime);
    }

    public void Draw()
    {
        var fps = Raylib.GetFPS();
        Raylib.ClearBackground(Color.Black);
        Raylib.DrawText("FPS:" + fps, 10, 10, 20, Color.White);
        
       // Raylib.DrawCircleV(_player.Position, 10, Color.Blue);
        _player.Draw();
        GameManager.Instance.ProjectileSystem.DrawProjectiles();
        
        GameManager.Instance.EnemySystem.DrawEnemies();
    
    }

    public void ExitState()
    {

    }
}
