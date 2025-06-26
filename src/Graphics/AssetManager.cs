using Raylib_cs;

namespace my_game.Graphics
{
    public static class AssetManager
    {
        private static string _assetRootPath = string.Empty; // Default to an empty string
        private static Dictionary<string, Image> _images = new();
        private static Image _defaultImage;
        //private static Texture2D _defaultTexture; // Initialize with a default value


        public static void SetDefaultImage(string filePath, bool filePathIsAbsolute = false)
        {
            var fullPath = filePath;
            
            if (!filePathIsAbsolute && !string.IsNullOrEmpty(_assetRootPath))
            {
                // Combine the asset root path with the relative file path
                fullPath = Path.Combine(_assetRootPath, filePath);
            }
            Console.WriteLine($"Path to default texture: {fullPath}");
            _defaultImage = Raylib.LoadImage(fullPath);
            //_defaultTexture = Raylib.LoadTexture(fullPath);
        }
        
        public static void SetRootPath(string assetRootPath)
        {
            _assetRootPath = assetRootPath;
        }
        
        // Load a texture into memory
        public static void LoadImage(string key, string filePath, bool filePathIsAbsolute = false)
        {
            var fullPath = filePath;
            
            if (!filePathIsAbsolute && !string.IsNullOrEmpty(_assetRootPath))
            {
                // Combine the asset root path with the relative file path
                fullPath = Path.Combine(_assetRootPath, filePath);
            }

            Console.WriteLine($"New key:{key}fullPath to texture: {fullPath}");
            if (!_images.ContainsKey(key))
            {
                var image = Raylib.LoadImage(fullPath);
                _images.TryAdd(key, image);
            }
            
        }

        // Retrieve an image by its key
        public static Image GetImage(string key)
        {
            if (!_images.ContainsKey(key))
            {
                Console.WriteLine($"Warning: Texture with key '{key}' not found. Returning default texture.");
            }
            return _images.GetValueOrDefault(key, _defaultImage);
        }


        // Unload all images
        public static void UnloadAllImages()
        {
            foreach (var image in _images.Values)
            {
                Raylib.UnloadImage(image);
            }
            _images.Clear();
        }
    }
}