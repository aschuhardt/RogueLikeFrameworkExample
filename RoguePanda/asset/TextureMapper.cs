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
                throw buildIncorrectAssetTypeError(id, imgAsset.assetType);
            }
        }

        private static InvalidAssetStreamAccessException buildIncorrectAssetTypeError(string id, AssetType t) {
            string msg = $"Incorrect asset type: Was expecting a Sprite, but the supplied asset name \"{id}\" refers to an asset of type \"{t.ToString()}\".";
            return new InvalidAssetStreamAccessException(msg);
        }
    }
}
