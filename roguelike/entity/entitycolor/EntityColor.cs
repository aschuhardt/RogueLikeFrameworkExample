namespace RoguePanda.entity.entitycolor {
    public struct DrawObjectColor {
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }
        public static DrawObjectColor createRGB(byte r, byte g, byte b) {
            bool valid = true;
            valid &= (r >= 0 && r <= 255);
            valid &= (g >= 0 && g <= 255);
            valid &= (b >= 0 && b <= 255);
            if (!valid) {
                throw new InvalidEntityColorValueException("Invalid R/G/B value provided.");
            } else {
                return new DrawObjectColor { R = r, B = b, G = g };
            }
        }
    }
}
