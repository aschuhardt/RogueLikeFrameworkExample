using roguelike.entity.entitycolor;

namespace roguelike.entity {
    class FlexibleEntity : EntityBase {
        public FlexibleEntity(string content) : this(content, EntityColor.createRGB(0, 0, 0), EntityColor.createRGB(0, 0, 0), 0.0f, 0.0f, 0.0f) { }
        public FlexibleEntity(string content, EntityColor foreColor, EntityColor backColor, float x, float y) : this(content, foreColor, backColor, x, y, 0.0f) { }
        public FlexibleEntity(string content, EntityColor foreColor, EntityColor backColor, float x, float y, float layer) : base() {
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
