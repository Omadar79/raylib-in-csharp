﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using my_game.Enemies;
using my_game.input;
using my_game.state;
using my_game.systems;
using my_game.utilities;
using Raylib_cs;

namespace my_game.Managers;

[SuppressMessage("ReSharper", "ConvertConstructorToMemberInitializers")]
public class GameManager
{
    private static GameManager? _instance; // Singleton instance of GameManager

    public  static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    // ---------------- Game Systems
    public readonly InputSystem inputSystem;
    public readonly ProjectileSystem projectileSystem;
    public readonly EnemySystem enemySystem;
    public readonly CollisionSystem collisionSystem;

    // ---------------- Managers
    public AssetManager assetManager;// = new AssetManager();
    public AudioManager audioManager;//  = new AudioManager();

    private LevelManager _levelManager;
    private StateManager _stateManager;

    private readonly int _screenWidth = 1024; // Default width
    private readonly int _screenHeight = 768; // Default height
    private readonly int _targetFPS = 60; // Default FPS

    private static readonly string SettingsFilePath = "game_settings.json";

    // -------------  Our Constructor sets up the rest of the game system managers
    public GameManager()
    {
        assetManager = new AssetManager();
        audioManager  = new AudioManager();

        _levelManager = new LevelManager();
        _stateManager = new StateManager();
        
        inputSystem = new InputSystem();
        projectileSystem = new ProjectileSystem();
        enemySystem = new EnemySystem();
        collisionSystem = new CollisionSystem();
    }

    /// <summary>
    /// Get the current game to see if we need to continue running the game loop.
    /// </summary>
    public bool IsGameRunning()
    {
        return !Raylib.WindowShouldClose();
    }
        
    /// <summary>
    /// Initializes Game settings, graphic assets, and audio.
    /// This method is called to set up the game environment before starting the game.
    /// </summary>
    public void StartGame()
    {
        InitializeGameSettings();
        assetManager.InitializeGraphicAssets();

        audioManager.InitializeAudio();
        
        // TODO Load Game Levels, and start state
        // Set initial game state for now
        _stateManager.SetState(new GameplayState());
        
    }
    
    /// <summary>
    /// Updates the game state.
    /// This method is called every frame to update the game logic from the current state.
    /// </summary>
    public void UpdateTick()
    {
        audioManager.UpdateAudioTick();
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

    /// <summary>
    /// Unloads all game resources and closes the game window.
    /// </summary>
    public void UnloadGame()
    {
        assetManager.UnloadAllImages();
        audioManager.UnloadAudio();
        Raylib.CloseWindow();
    }
    
    /// <summary>
    /// Initializes general, user-defined game settings.
    /// </summary>
    private void InitializeGameSettings()
    {
        Raylib.InitWindow(_screenWidth, _screenHeight, "Shmup Combat");
        Raylib.SetTargetFPS(_targetFPS);
    }
    
    private GameSettings Load()
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
    private void Save()
    {
        var jsonContent = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsFilePath, jsonContent);
    }

}