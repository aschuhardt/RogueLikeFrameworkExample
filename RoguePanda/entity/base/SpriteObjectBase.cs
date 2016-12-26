using RoguePanda.entity.color;

namespace RoguePanda.entity {
    internal class SpriteObjectBase : ISpriteObject {
        public string assetID { get; set; }
        public EntityColor color { get; set; }
        public byte alpha { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public float rotation { get; set; }
        public float scaleX { get; set; }
        public float scaleY { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int texPosX { get; set; }
        public int texPosY { get; set; }
        public float layer { get; set; }

        protected SpriteObjectBase() { }
    }
}
