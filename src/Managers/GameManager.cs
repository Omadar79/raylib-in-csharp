using System.Text.Json;
using my_game.state;
using my_game.utilities;

namespace my_game.managers;

public class GameManager
{
    private static GameManager _instance = null!;
    
    private LevelManager _levelManager ;
    private GameStateManager _stateManager;
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

    private GameManager()
    {
        _levelManager = new LevelManager();
        _stateManager = new GameStateManager();
    }


    public void UpdateTick()
    {
        _stateManager.Update();
    }

    public void DrawTick()
    {
        _stateManager.Draw();
    }
    
    
    public void StartGame()
    {
        //TODO Game Start logic might go through Logo and then to Main Menu
        
        //TODO Load Game Levels
        
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

    public void Save()
    {
        var jsonContent = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SettingsFilePath, jsonContent);
    }

}