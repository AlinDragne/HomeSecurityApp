using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeSecurityApp
{
    public class ImageService
    {
        private readonly string _framesFolderPath;

        public ImageService(string framesFolderPath)
        {
            _framesFolderPath = framesFolderPath;
        }

        public IEnumerable<string> GetImageFileNames()
        {
            // Ensure the directory exists
            if (!Directory.Exists(_framesFolderPath))
            {
                throw new DirectoryNotFoundException($"The directory {_framesFolderPath} could not be found.");
            }

            // Retrieve the file names of all images in the directory
            return Directory.EnumerateFiles(_framesFolderPath, "*.jpg")
                .Select(Path.GetFileName)
                .Where(name => name != null)!;  // Filter out null file names
        }

    }
}
