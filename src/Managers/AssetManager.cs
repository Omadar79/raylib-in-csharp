using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Raylib_cs;

namespace my_game.Graphics
{
    public class AssetManager
    {
        private const string RESOURCE_ROOT_DIRECTORY = "C:\\Users\\Dustin\\RayLibProjects\\raylib-in-csharp\\";
        
        private static AssetManager _instance = null!;
        private string _assetRootPath = string.Empty; // Default to an empty string
        private Dictionary<string, Image> _images = new();
        private Image _defaultImage;
        //private Texture2D _defaultTexture; // Initialize with a default value

        public static AssetManager Instance
        {
            get
            {

                return _instance ??= new AssetManager();

            }
        }
        
        
        public void SetDefaultImage(string filePath, bool filePathIsAbsolute = false)
        {
            string fullPath = filePath;
            string rootPath;
            if (string.IsNullOrEmpty(_assetRootPath))
            {
                // If the asset root path is not set, use the resource root directory
                rootPath = RESOURCE_ROOT_DIRECTORY;
            }
            else
            {
                rootPath = _assetRootPath;
            }
            
            if (!filePathIsAbsolute)
            {
                // Combine the asset root path with the relative file path
                fullPath = Path.Combine(rootPath, filePath);
            }
            Console.WriteLine($"Path to default texture: {fullPath}");
            _defaultImage = Raylib.LoadImage(fullPath);
            //_defaultTexture = Raylib.LoadTexture(fullPath);
        }
        
        public void SetRootPath(string assetRootPath)
        {
            _assetRootPath = assetRootPath;
        }
        
        public void ResetRootPath()
        {
            _assetRootPath = string.Empty; // Reset to default empty string
        }
        
        public string GetRootPath()
        {
            if (string.IsNullOrEmpty(_assetRootPath))
            {
                return RESOURCE_ROOT_DIRECTORY;
            }
            else
            {
                return _assetRootPath;
            }
        }
        // Load a texture into memory
        public void LoadImage(string key, string filePath, bool filePathIsAbsolute = false)
        {
            string fullPath = filePath;
            string rootPath;
            if (string.IsNullOrEmpty(_assetRootPath))
            {
                // If the asset root path is not set, use the resource root directory
                rootPath = RESOURCE_ROOT_DIRECTORY;
            }
            else
            {
                rootPath = _assetRootPath;
            }
            
            if (!filePathIsAbsolute)
            {
                // Combine the asset root path with the relative file path
                fullPath = Path.Combine(rootPath, filePath);
            }

            Console.WriteLine($"New key:{key}fullPath to texture: {fullPath}");
            if (!_images.ContainsKey(key))
            {
                var image = Raylib.LoadImage(fullPath);
                _images.TryAdd(key, image);
            }
            
        }

        // Retrieve an image by its key
        public Image GetImage(string key)
        {
            if (!_images.ContainsKey(key))
            {
                Console.WriteLine($"Warning: Texture with key '{key}' not found. Returning default texture.");
            }
            return _images.GetValueOrDefault(key, _defaultImage);
        }


        // Unload all images
        public void UnloadAllImages()
        {
            foreach (var image in _images.Values)
            {
                Raylib.UnloadImage(image);
            }
            _images.Clear();
        }
        
        
    }
}