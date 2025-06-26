using System.Numerics;
using my_game.player;
using Raylib_cs;

namespace my_game.state;

public class GameplayState : IGameState
{

    private Random _random = new Random();
    private Player _player = new Player(new Vector2(400, 240));
    
    public void Update()
    {
        var deltaTime = Raylib.GetFrameTime();
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        var playerPos = _player.Position;
        playerPos  += Program.InputSystem.GetMovement();
        
        playerPos.X = Math.Clamp(playerPos.X, 0, screenWidth);
        playerPos.Y = Math.Clamp(playerPos.Y, 0, screenHeight);
        _player.Position = playerPos;
        
        // Shoot bullets
        if (Program.InputSystem.IsShooting())
        {
 
            var aimDirection = Program.InputSystem.GetAimDirection(_player.Position);
            Program.ProjectileSystem.AddProjectile(_player.Position, aimDirection);


            _player.Rotation = Program.InputSystem.VectorToAimAngle(aimDirection);

        }
        Program.ProjectileSystem.UpdateProjectiles(deltaTime, Program.EnemySystem.GetActiveEnemies());
     
        // Spawn enemies randomly
        if (_random.Next(100) < 2)
        {
            var randomEnemySpawnPosition = new Vector2(_random.Next(0, screenWidth), 5);
            var enemyVelocity = new Vector2(0, 1); // Enemies move downwards
            var enemySize = new Vector2(170, 102); // Size of the enemy
            Program.EnemySystem.AddEnemy("enemy6",randomEnemySpawnPosition, enemyVelocity,enemySize );
        }
        Program.EnemySystem.UpdateEnemies(deltaTime);
    }

    public void Draw()
    {
        var fps = Raylib.GetFPS();
        Raylib.ClearBackground(Color.Black);
        Raylib.DrawText("FPS:" + fps, 10, 10, 20, Color.White);
        
       // Raylib.DrawCircleV(_player.Position, 10, Color.Blue);
        _player.Draw();
        Program.ProjectileSystem.DrawProjectiles();
        
        Program.EnemySystem.DrawEnemies();
    
    }
}
