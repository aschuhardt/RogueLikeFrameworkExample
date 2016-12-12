using RoguePanda.entity.entitycolor;

namespace RoguePanda.entity {
    class FlexibleEntity : DrawObjectBase {
        public FlexibleEntity(string content) : this(content, DrawObjectColor.createRGB(0, 0, 0), DrawObjectColor.createRGB(0, 0, 0), 0.0f, 0.0f, 0.0f) { }
        public FlexibleEntity(string content, DrawObjectColor foreColor, DrawObjectColor backColor, float x, float y) : this(content, foreColor, backColor, x, y, 0.0f) { }
        public FlexibleEntity(string content, DrawObjectColor foreColor, DrawObjectColor backColor, float x, float y, float layer) : base() {
            _content = content;
            _foreColor = foreColor;
            _backColor = backColor;
            _x = x;
            _y = y;
            _layer = layer;
        }
        private FlexibleEntity() { }
    }
}
