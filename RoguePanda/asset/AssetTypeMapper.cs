using System.Linq;

namespace RoguePanda.asset {
    internal class AssetTypeMapper {
        //add more of these for future supported asset file types
        private static string[] imageExtensions = { ".jpg", ".png", ".tiff" };
        private static string[] audioExtensions = { ".ogg", ".flac", ".wav" };

        private AssetTypeMapper() {

        }

        public static IAsset getAsset(string fileName) {
            //check whether the filetype fits any of our supported formats
            if (imageExtensions.Any((x) => fileName.EndsWith(x))) {
                return new SpriteAsset(fileName);
            } else if (audioExtensions.Any((x) => fileName.EndsWith(x))) {
                return new AudioAsset(fileName);
            } else {
                string msg = $"File is of an unknown/unsupported format: \"{fileName}\".";
                throw new UnknownFileTypeException(msg);
            }
        }
    }
}
