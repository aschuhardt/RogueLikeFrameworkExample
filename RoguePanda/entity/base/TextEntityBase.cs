using RoguePanda.entity.color;

namespace RoguePanda.entity {
    internal class TextEntityBase : ITextEntity {
        public string content { get; set; }
        public EntityColor foreColor { get; set; }
        public EntityColor backColor { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float layer { get; set; }

        protected TextEntityBase() { }
    }
}
