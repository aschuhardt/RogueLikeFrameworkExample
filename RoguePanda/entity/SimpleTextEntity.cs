using RoguePanda.entity.color;

namespace RoguePanda.entity {
    internal class SimpleTextEntity : TextEntityBase {
        public SimpleTextEntity(string content) : this(content, EntityColor.createRGB(0, 0, 0), EntityColor.createRGB(0, 0, 0), 0.0f, 0.0f, 0.0f) { }
        public SimpleTextEntity(string content, EntityColor foreColor, EntityColor backColor, float x, float y) : this(content, foreColor, backColor, x, y, 0.0f) { }
        public SimpleTextEntity(string content, EntityColor foreColor, EntityColor backColor, float x, float y, float layer) : base() {
            this.content = content;
            this.foreColor = foreColor;
            this.backColor = backColor;
            this.x = x;
            this.y = y;
            this.layer = layer;
        }
        private SimpleTextEntity() { }
    }
}
