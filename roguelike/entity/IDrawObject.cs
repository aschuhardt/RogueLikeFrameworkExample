using RoguePanda.entity.entitycolor;

namespace RoguePanda.entity {
    interface IDrawObject {
        string contents { get; }
        EntityColor foreColor { get; }
        EntityColor backColor { get; }
        float x { get; set; }
        float y { get; set; }
        float layer { get; set; }
    }
}
