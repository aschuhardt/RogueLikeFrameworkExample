using RoguePanda.entity.color;

namespace RoguePanda.entity {
    public interface ITextObject {
        string contents { get; }
        EntityColor foreColor { get; }
        EntityColor backColor { get; }
        float x { get; set; }
        float y { get; set; }
        float layer { get; set; }
    }
}
