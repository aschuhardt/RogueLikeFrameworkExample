using RoguePanda.asset;
using SFML.Graphics;

namespace RoguePanda {
    internal class TextureMapper {

        private TextureMapper() { }

        public static Texture getTexture(string id) {
            IAsset imgAsset = AssetCache.cachedAsset(id);
            if (imgAsset.assetType == AssetType.Sprite && imgAsset is SpriteAsset) {
                return ((SpriteAsset)imgAsset).texture;
            } else {
                string msg = $"Incorrect asset type: Was expecting a Sprite, but the supplied asset name \"{id}\" refers to an asset of type \"{imgAsset.assetType.ToString()}\".";
                throw new InvalidAssetStreamAccessException(msg);
            }
        }
    }
}
