using my_game.Managers;

namespace my_game;

public static class Program
{
    // Singleton instances for Game Manager that handles all Game Logic, State Management, and Systems
    private static GameManager GameManager => GameManager.Instance;
    
    [STAThread]  // needed for Windows Forms compatibility
    public static void Main()
    {
        GameManager.StartGame();  // Initialize game state and managers
        
        // -----------------PRIMARY GAME LOOP
        while (GameManager.IsGameRunning())
        {
            GameManager.UpdateTick();  // 1. Game State, Audio, and Input Update Tick
            
            GameManager.DrawTick();    // 2.  Graphics Update Tick
        }

        GameManager.UnloadGame();
    }
   
}

