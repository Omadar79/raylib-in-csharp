using System.Text.Json;

namespace my_game.utilities;

public class GameSettings
{
    public int ScreenWidth { get; set; } = 1024; // Default width
    public int ScreenHeight { get; set; } = 768; // Default height
    public int TargetFPS { get; set; } = 60; // Default FPS

    private static readonly string SettingsFilePath = "game_settings.json";

    public static GameSettings Load()
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