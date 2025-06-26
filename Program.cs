using Raylib_cs;
using System.IO;
using my_game.enemies;
using my_game.Graphics;
using my_game.input;
using my_game.player;
using my_game.state;


namespace my_game;

public static class Program
{
    // Singleton instances for various systems
    public static readonly InputSystem InputSystem = new InputSystem();
    public static readonly ProjectileSystem ProjectileSystem = new ProjectileSystem();
    public static readonly EnemySystem EnemySystem = new EnemySystem();


    public const string ResourceRootDirectory = "C:\\Users\\Dustin\\RayLibProjects\\raylib-in-csharp\\";

    //--- Game Constants and Global Variables ---
    private static readonly int _screenWidth = 1024;
    private static readonly int _screenHeight = 768;
    
    private static GameStateManager _stateManager = new GameStateManager();
    
   // private static Music _music = Raylib.LoadMusicStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "music", "mission_plausible.ogg"));

    private static Music _music;
    
    [STAThread]  // needed for Windows Forms compatibility
    public static void Main()
    {
        
        InitializeGame();
        
        // PRIMARY GAME LOOP
        while (!Raylib.WindowShouldClose())
        {
            Raylib.UpdateMusicStream(_music);
            _stateManager.Update();
           
            Raylib.BeginDrawing();
            _stateManager.Draw();
            Raylib.EndDrawing();
        }

        UnloadGame();
    }

    private static void InitializeGame()
    {
        InitializeAssets();
        
        Raylib.InitWindow(_screenWidth, _screenHeight, "Shmup Combat");
        Raylib.SetTargetFPS(60);
        
        _stateManager.SetState(new GameplayState());
        InitializeAudio();


    }

    private static void InitializeAssets()
    {
        AssetManager.SetRootPath(ResourceRootDirectory);
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
        var filePath = Path.Combine(ResourceRootDirectory, "resources","music", "mission_plausible.ogg");
        _music = Raylib.LoadMusicStream(filePath);
        
        // Play music
        Raylib.PlayMusicStream(_music);
        Raylib.SetMusicVolume(_music, 1.0f);
    }
}

