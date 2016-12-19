using RoguePanda.drawobject.color;

namespace RoguePanda.drawobject {
    public interface IDrawObject {
        string contents { get; }
        DrawObjectColor foreColor { get; }
        DrawObjectColor backColor { get; }
        float x { get; set; }
        float y { get; set; }
        float layer { get; set; }
    }
}
