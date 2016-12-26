using SFML.Graphics;
using System.IO;

namespace RoguePanda.asset {
    internal class Sprite : AssetBase {
        Texture _texture;

        public Texture texture {
            get {
                if (_texture == null) {
                    using (MemoryStream ms = fileStream) {
                        _texture = new Texture(ms);
                    }
                }
                return _texture;
            }
        }

        public Sprite(string filePath) : base(filePath) {

        }

        protected override AssetType getAssetType() {
            return AssetType.Sprite;
        }
    }
}
