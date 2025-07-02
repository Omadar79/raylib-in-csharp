using Raylib_cs;

namespace my_game.Managers;

public class AudioManager
{
    private Music _music;
    
    public void InitializeAudio()
    {
        // Initialize audio system
        Raylib.InitAudioDevice();

        var defaultRootPath = GameManager.Instance.assetManager.GetRootPath();
        // Load music file
        var filePath = Path.Combine(defaultRootPath, "resources","music", "mission_plausible.ogg");
        _music = Raylib.LoadMusicStream(filePath);
        
        // Play music
        //Raylib.PlayMusicStream(_music);
        Raylib.SetMusicVolume(_music, 1.0f);
      
    }

    public void PlayMusicTrack()
    {
       
        Raylib.PlayMusicStream(_music);
        
    }
    
    public void UpdateAudioTick()
    {
        // Logic Update Tick
        Raylib.UpdateMusicStream(_music);
    }

    public void StopMusicTrack()
    {
        Raylib.StopMusicStream(_music); 
    }
    
    public void UnloadAudio()
    {
       // Raylib.UnloadAudioStream(_music);
        Raylib.CloseAudioDevice(); // Close audio device when done
    }
}