using RoguePanda.entity.color;

namespace RoguePanda.entity {
    internal class SimpleSpriteEntity : SpriteEntityBase {
        public SimpleSpriteEntity(string assetName, int width, int height, float layer) : base() {
            this.assetID = assetName;
            this.width = width;
            this.height = height;
            this.color = EntityColor.createRGB(255, 255, 255);
            this.alpha = 255;
            this.rotation = 0.0f;
            this.scaleX = 1.0f;
            this.scaleY = 1.0f;
            this.texPosX = 0;
            this.texPosY = 0;
            this.layer = layer;
        }
    }
}
