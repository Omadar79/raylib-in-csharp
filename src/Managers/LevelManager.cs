using System.Numerics;

namespace my_game.Managers;

public class LevelManager
{
    private List<Level> _levels = new();
    private int _currentLevelIndex = 0;
    
    
    public LevelManager()
    {
        LoadLevels();
    }
    
    private void LoadLevels()
    {
        // Example levels
        _levels.Add(new Level("Level 1", "background1.png", new List<Vector2> { new Vector2(100, 100), new Vector2(200, 200) }, 10));
        _levels.Add(new Level("Level 2", "background2.png", new List<Vector2> { new Vector2(300, 300), new Vector2(400, 400) }, 15));
    }

    public Level GetCurrentLevel() => _levels[_currentLevelIndex];

    public void SetLevel(int index)
    {
        if (index >= 0 && index < _levels.Count)
        {
            _currentLevelIndex = index;
        }
    }

    public List<Level> GetAllLevels() => _levels;
}


public class Level
{
    public string Name { get; set; }
    public string BackgroundAsset { get; set; }
    public List<Vector2> EnemySpawnPoints { get; set; }
    public int EnemyCount { get; set; }

    public Level(string name, string backgroundAsset, List<Vector2> enemySpawnPoints, int enemyCount)
    {
        Name = name;
        BackgroundAsset = backgroundAsset;
        EnemySpawnPoints = enemySpawnPoints;
        EnemyCount = enemyCount;
    }
}