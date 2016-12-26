namespace RoguePanda.entity.color {
    public struct EntityColor {
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }
        public static EntityColor createRGB(byte r, byte g, byte b) {
            bool valid = true;
            valid &= (r >= 0 && r <= 255);
            valid &= (g >= 0 && g <= 255);
            valid &= (b >= 0 && b <= 255);
            if (!valid) {
                throw new InvalidEntityColorValueException("Invalid R/G/B value provided.");
            } else {
                return new EntityColor { R = r, B = b, G = g };
            }
        }
    }
}
