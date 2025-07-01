using Raylib_cs;
using my_game.enemies;
using my_game.Graphics;
using my_game.input;
using my_game.managers;
using my_game.state;
using my_game.systems;
using my_game.utilities;


namespace my_game;

public static class Program
{
    // Singleton instances for managers (Can track state)
    public static GameManager GameManager => GameManager.Instance;
    public static AssetManager AssetManager => AssetManager.Instance;
   // public static LevelManager LevelManager => LevelManager.Instance;
    
    // static instances for various systems (not stateful)
    public static readonly InputSystem InputSystem = new InputSystem();
    public static readonly ProjectileSystem ProjectileSystem = new ProjectileSystem();
    public static readonly EnemySystem EnemySystem = new EnemySystem();
    public static readonly CollisionSystem CollisionSystem = new CollisionSystem();
    
    // private static Music _music = Raylib.LoadMusicStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "music", "mission_plausible.ogg"));

    private static Music _music;
    
    [STAThread]  // needed for Windows Forms compatibility
    public static void Main()
    {
        InitializeGame();
        GameManager.StartGame();
        // -----------------PRIMARY GAME LOOP
        while (!Raylib.WindowShouldClose())
        {
            // Logic Update Tick
            Raylib.UpdateMusicStream(_music);
            GameManager.UpdateTick();
           
            // Drawing Update Tick
            Raylib.BeginDrawing();
            GameManager.DrawTick();
            Raylib.EndDrawing();
        }

        UnloadGame();
    }

    private static void InitializeGame()
    {
        InitializeAssets();
        
        Raylib.InitWindow(GameManager.ScreenWidth, GameManager.ScreenHeight, "Shmup Combat");
        Raylib.SetTargetFPS(GameManager.TargetFPS);
        
        InitializeAudio();
    }

    private static void InitializeAssets()
    {
        AssetManager.SetDefaultImage(Path.Combine("resources", "sprites", "defaultTexture.png"));
       
        AssetManager.LoadImage("tank_base", Path.Combine("resources", "sprites", "ACS_Base.png"));
        AssetManager.LoadImage("tank_turret", Path.Combine("resources", "sprites", "ACS_Tower.png"));
        AssetManager.LoadImage("enemy6", Path.Combine("resources", "sprites", "enemy6.png"));
       
        
        // Load enemy sprites
        //EnemyVisuals.RegisterSprite("enemy_ship", AssetManager.GetTexture("enemy_ship"));

    }

    private static void UnloadGame()
    {
        AssetManager.UnloadAllImages();
        Raylib.StopMusicStream(_music);
        
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }
    
    private static void InitializeAudio()
    {
       
        // Initialize audio device
        Raylib.InitAudioDevice();
        
        // Load music file
        var filePath = Path.Combine(AssetManager.GetRootPath(), "resources","music", "mission_plausible.ogg");
        _music = Raylib.LoadMusicStream(filePath);
        
        // Play music
        Raylib.PlayMusicStream(_music);
        Raylib.SetMusicVolume(_music, 1.0f);
    }
}

