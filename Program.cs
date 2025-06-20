using System.Numerics;
using Raylib_cs;

namespace my_game
{
    public static class Program
    {
        //--- Game Constants and Global Variables ---
        private static readonly int _screenWidth = 800;
        private static readonly int _screenHeight = 480;

        private static Vector2 _playerPosition = new Vector2(_screenWidth / 2, _screenHeight - 50);
        private static List<Vector2> _bullets = new List<Vector2>();
        private static List<Vector2> _enemies = new List<Vector2>();
        private static Random _random = new Random();

        [STAThread]  // needed for Windows Forms compatibility
        public static void Main()
        {
            InitializeGame();
            
            // PRIMARY GAME LOOP
            while (!Raylib.WindowShouldClose())
            {
                // ----------- Update Logic
                UpdatePlayer();
                UpdateBullets();
                UpdateEnemies();

                // ------------ Draw
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                DrawPlayer();
                DrawBullets();
                DrawEnemies();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        private static void InitializeGame()
        {
            Raylib.InitWindow(_screenWidth, _screenHeight, "Shmup Combat");
            Raylib.SetTargetFPS(60);
            
            
        }
        
        private static void UpdatePlayer()
        {
            if (Raylib.IsKeyDown(KeyboardKey.A)) _playerPosition.X -= 5;
            if (Raylib.IsKeyDown(KeyboardKey.D)) _playerPosition.X += 5;
            if (Raylib.IsKeyDown(KeyboardKey.W)) _playerPosition.Y -= 5;
            if (Raylib.IsKeyDown(KeyboardKey.S)) _playerPosition.Y += 5;

            // Keep player within screen bounds
            _playerPosition.X = Math.Clamp(_playerPosition.X, 0, _screenWidth);
            _playerPosition.Y = Math.Clamp(_playerPosition.Y, 0, _screenHeight);

            // Shoot bullets
            if (Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                _bullets.Add(new Vector2(_playerPosition.X, _playerPosition.Y - 10));
            }
        }
        static void UpdateBullets()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                _bullets[i] = new Vector2(_bullets[i].X, _bullets[i].Y - 10);

                // Remove bullets that go off-screen
                if (_bullets[i].Y < 0)
                {
                    _bullets.RemoveAt(i);
                }
            }
        }
        static void UpdateEnemies()
        {
            // Spawn enemies randomly
            if (_random.Next(100) < 2)
            {
                _enemies.Add(new Vector2(_random.Next(0, _screenWidth), 0));
            }

            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                _enemies[i] = new Vector2(_enemies[i].X, _enemies[i].Y + 2);

                // Remove enemies that go off-screen
                if (_enemies[i].Y > _screenHeight)
                {
                    _enemies.RemoveAt(i);
                }
            }
        }
        private static void DrawPlayer()
        {
            Raylib.DrawCircleV(_playerPosition, 10, Color.Blue);
        }
            
        
        static void DrawBullets()
        {
            foreach (var bullet in _bullets)
            {
                Raylib.DrawCircleV(bullet, 5, Color.Yellow);
            }
        }
        
        static void DrawEnemies()
        {
            foreach (var enemy in _enemies)
            {
                Raylib.DrawRectangleV(enemy, new Vector2(20, 20), Color.Red);
            }
        }
    }

}