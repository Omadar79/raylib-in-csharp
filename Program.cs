using Raylib_cs;
using my_game.Enemies;
using my_game.input;
using my_game.Managers;
using my_game.systems;


namespace my_game;

public static class Program
{
    // Singleton instances for managers (Can track state)
    private static GameManager GameManager => GameManager.Instance;
 
    
    [STAThread]  // needed for Windows Forms compatibility
    public static void Main()
    {
        GameManager.StartGame();  // Initialize game state and managers
        
        // -----------------PRIMARY GAME LOOP
        while (!Raylib.WindowShouldClose())
        {
            GameManager.UpdateTick();  // 1. Game State, Audio, and Input Update Tick
            
            GameManager.DrawTick();    // 2.  Graphics Update Tick
        }

        GameManager.UnloadGame();
    }



    
   
}

