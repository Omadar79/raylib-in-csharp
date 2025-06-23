using System.Numerics;
using Raylib_cs;

namespace my_game.state
{
    public class GameplayState : IGameState
    {
        private Vector2 _playerPosition = new Vector2(400, 240);
        private Random _random = new Random();
       
        
        public void Update()
        {
            var deltaTime = Raylib.GetFrameTime();
            int screenWidth = Raylib.GetScreenWidth();
            int screenHeight = Raylib.GetScreenHeight();

            _playerPosition += Program.InputSystem.GetMovement();
            
            _playerPosition.X = Math.Clamp(_playerPosition.X, 0, screenWidth);
            _playerPosition.Y = Math.Clamp(_playerPosition.Y, 0, screenHeight);
             
            // Shoot bullets
            if (Program.InputSystem.IsShooting())
            {
                Program.ProjectileSystem.AddProjectile(_playerPosition
                     , Program.InputSystem.GetFireDirection(_playerPosition));

            }
            Program.ProjectileSystem.UpdateProjectiles(deltaTime, Program.EnemySystem.GetActiveEnemies());
         
            // Spawn enemies randomly
            if (_random.Next(100) < 2)
            {
                var randomEnemySpawnPosition = new Vector2(_random.Next(0, screenWidth), 5);
                var enemyVelocity = new Vector2(0, 1); // Enemies move downwards
                var enemySize = new Vector2(30, 30); // Size of the enemy
                Program.EnemySystem.AddEnemy(randomEnemySpawnPosition, enemyVelocity,enemySize );
            }
            Program.EnemySystem.UpdateEnemies(deltaTime);
        }

        public void Draw()
        {
            var fps = Raylib.GetFPS();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("FPS:" + fps, 10, 10, 20, Color.White);
            
            Raylib.DrawCircleV(_playerPosition, 10, Color.Blue);
            
            Program.ProjectileSystem.DrawProjectiles();
            
            Program.EnemySystem.DrawEnemies();
        
        }
    }
}