using SFML.Audio;
using RoguePanda.asset;

namespace RoguePanda {
    internal class AudioMapper {

        private AudioMapper() { }

        public static Sound getSound(string id) {
            IAsset sndAsset = AssetCache.cachedAsset(id);
            if (sndAsset.assetType == AssetType.Audio && sndAsset is AudioAsset) {
                return ((AudioAsset)sndAsset).sound;
            } else {
                throw buildIncorrectAssetTypeError(id, sndAsset.assetType);
            }
        }

        public static Music getMusic(string id) {
            IAsset sndAsset = AssetCache.cachedAsset(id);
            if (sndAsset.assetType == AssetType.Audio && sndAsset is AudioAsset) {
                return ((AudioAsset)sndAsset).music;
            } else {
                throw buildIncorrectAssetTypeError(id, sndAsset.assetType);
            }
        }

        private static InvalidAssetStreamAccessException buildIncorrectAssetTypeError(string id, AssetType t) {
            string msg = $"Incorrect asset type: Was expecting an Audio asset, but the supplied asset name \"{id}\" refers to an asset of type \"{t.ToString()}\".";
            return new InvalidAssetStreamAccessException(msg);
        }
    }
}
