using roguelike.entity.entitycolor;

namespace roguelike.entity {
    interface IEntity {
        string contents { get; }
        EntityColor foreColor { get; }
        EntityColor backColor { get; }
        float x { get; set; }
        float y { get; set; }
        float layer { get; set; }
    }
}
