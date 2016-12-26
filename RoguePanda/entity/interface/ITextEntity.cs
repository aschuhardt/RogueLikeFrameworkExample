using RoguePanda.entity.color;

namespace RoguePanda.entity {
    public interface ITextEntity {
        string content { get; set; }
        EntityColor foreColor { get; set; }
        EntityColor backColor { get; set; }
        float x { get; set; }
        float y { get; set; }
        float layer { get; set; }
    }
}
