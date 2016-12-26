using RoguePanda.entity.color;

namespace RoguePanda.entity {
    internal class FlexibleTextObject : TextObjectBase {
        public FlexibleTextObject(string content) : this(content, EntityColor.createRGB(0, 0, 0), EntityColor.createRGB(0, 0, 0), 0.0f, 0.0f, 0.0f) { }
        public FlexibleTextObject(string content, EntityColor foreColor, EntityColor backColor, float x, float y) : this(content, foreColor, backColor, x, y, 0.0f) { }
        public FlexibleTextObject(string content, EntityColor foreColor, EntityColor backColor, float x, float y, float layer) : base() {
            _content = content;
            _foreColor = foreColor;
            _backColor = backColor;
            _x = x;
            _y = y;
            _layer = layer;
        }
        private FlexibleTextObject() { }
    }
}
