using System.Text.Json;
using my_game.Enemies;
using my_game.input;
using my_game.state;
using my_game.systems;
using my_game.utilities;
using Raylib_cs;

namespace my_game.Managers;

public class GameManager
{
    // ---------------- Singleton instance of GameManager
    private static GameManager _instance = null!;
    
    // ---------------- Managers
    private LevelManager _levelManager; 
    private GameStateManager _stateManager;
    private AudioManager _audioManager;
    
    
    public AssetManager assetManager;

    
    // ---------------- Game Systems
    public readonly InputSystem InputSystem;
    public readonly ProjectileSystem ProjectileSystem;
    public readonly EnemySystem EnemySystem;
    public readonly CollisionSystem CollisionSystem;
    
    public int ScreenWidth { get; set; } = 1024; // Default width
    public int ScreenHeight { get; set; } = 768; // Default height
    public int TargetFPS { get; set; } = 60; // Default FPS

    private static readonly string SettingsFilePath = "game_settings.json";
    
    public static GameManager Instance
    {
        get
        {
            return _instance ??= new GameManager();
        }
        
    }

    // Our Constructor sets up the rest of the game system managers
    private GameManager()
    {
        InputSystem = new InputSystem();
        ProjectileSystem = new ProjectileSystem();
        EnemySystem = new EnemySystem();
        CollisionSystem = new CollisionSystem();

        assetManager = new AssetManager(); // Singleton instance of AssetManager
        _levelManager = new LevelManager();
        _stateManager = new GameStateManager();
        _audioManager = new AudioManager();
    }


    private void InitializeGraphicAssets()
    {
        assetManager.InitializeGraphicAssets();
    }
    
    
    private void InitializeGameSettings()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Shmup Combat");
        Raylib.SetTargetFPS(TargetFPS);
    }

    
    /// <summary>
    /// Updates the game state.
    /// This method is called every frame to update the game logic from the current state.
    /// </summary>
    public void UpdateTick()
    {
        _audioManager.UpdateAudioTick();
        _stateManager.UpdateStateTick();
    }

    /// <summary>
    /// Draws the current game state.
    /// This method is called every frame to render the game visuals from the current state.
    /// </summary>
    public void DrawTick()
    {
        Raylib.BeginDrawing();
        _stateManager.DrawStateTick();
        Raylib.EndDrawing();
    }
    
    public void StartGame()
    {
        
        InitializeGameSettings();
        InitializeGraphicAssets();
        _audioManager.InitializeAudio();
        
        // TODO Load Game Levels, and start state
        // Set initial game state for now
        _stateManager.SetState(new GameplayState());
        
    }
    
    public GameSettings Load()
    {
        if (File.Exists(SettingsFilePath))
        {
            var jsonContent = File.ReadAllText(SettingsFilePath);
            return JsonSerializer.Deserialize<GameSettings>(jsonContent) ?? new GameSettings();
        }
        else
        {
            var defaultSettings = new GameSettings();
            defaultSettings.Save();
            return defaultSettings;
        }
    }
    
    public void UnloadGame()
    {
        assetManager.UnloadAllImages();
        _audioManager.UnloadAudio();

        Raylib.CloseWindow();
    }
    
    public void Save()
    {
        var jsonContent = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsFilePath, jsonContent);
    }

}