using System.Linq;

namespace RoguePanda.asset {
    internal class AssetTypeMapper {
        //add more of these for future supported asset file types
        private static string[] imageExtensions = { ".jpg", ".png", ".tiff" };

        private AssetTypeMapper() {

        }

        public static IAsset getAsset(string fileName) {
            //check whether the filetype fits any of our supported formats
            if (imageExtensions.Any((x) => fileName.EndsWith(x))) {
                //add more file types here when engine supports different asset types
                return new Sprite(fileName);
            } else {
                string msg = $"File is of an unknown/unsupported format: \"{fileName}\".";
                throw new UnknownFileTypeException(msg);
            }
        }
    }
}
