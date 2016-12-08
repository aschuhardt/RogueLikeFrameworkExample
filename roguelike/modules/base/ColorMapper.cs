using roguelike.entity.entitycolor;

namespace roguelike.modules {
    class ColorMapper {
        private const int DEFAULT = 255;

        //border
        private const int BORDER_FORE_R = 10;
        private const int BORDER_FORE_G = 33;
        private const int BORDER_FORE_B = 22;
        private const int BORDER_BACK_R = 0;
        private const int BORDER_BACK_G = 0;
        private const int BORDER_BACK_B = 0;

        //dark text
        private const int DARKTEXT_FORE_R = 76;
        private const int DARKTEXT_FORE_G = 103;
        private const int DARKTEXT_FORE_B = 123;
        private const int DARKTEXT_BACK_R = 0;
        private const int DARKTEXT_BACK_G = 0;
        private const int DARKTEXT_BACK_B = 0;

        //light text
        private const int LIGHTTEXT_FORE_R = 192;
        private const int LIGHTTEXT_FORE_G = 158;
        private const int LIGHTTEXT_FORE_B = 113;
        private const int LIGHTTEXT_BACK_R = 0;
        private const int LIGHTTEXT_BACK_G = 0;
        private const int LIGHTTEXT_BACK_B = 0;

        //title
        private const int TITLE_FORE_R = 192;
        private const int TITLE_FORE_G = 158;
        private const int TITLE_FORE_B = 113;
        private const int TITLE_BACK_R = 10;
        private const int TITLE_BACK_G = 33;
        private const int TITLE_BACK_B = 23;

        private ColorMapper() { }
        public static EntityColor getColor(Colors c) {
            EntityColor ret;

            switch (c) {
                case Colors.Border_ForeColor:
                    ret = EntityColor.createRGB(BORDER_FORE_R, BORDER_FORE_G, BORDER_FORE_B);
                    break;
                case Colors.DarkText_ForeColor:
                    ret = EntityColor.createRGB(DARKTEXT_FORE_R, DARKTEXT_FORE_G, DARKTEXT_FORE_B);
                    break;
                case Colors.LightText_ForeColor:
                    ret = EntityColor.createRGB(LIGHTTEXT_FORE_R, LIGHTTEXT_FORE_G, LIGHTTEXT_FORE_B);
                    break;
                case Colors.Border_BackColor:
                    ret = EntityColor.createRGB(BORDER_BACK_R, BORDER_BACK_G, BORDER_BACK_B);
                    break;
                case Colors.DarkText_BackColor:
                    ret = EntityColor.createRGB(DARKTEXT_BACK_R, DARKTEXT_BACK_G, DARKTEXT_BACK_B);
                    break;
                case Colors.LightText_BackColor:
                    ret = EntityColor.createRGB(LIGHTTEXT_BACK_R, LIGHTTEXT_BACK_G, LIGHTTEXT_BACK_B);
                    break;
                case Colors.Title_ForeColor:
                    ret = EntityColor.createRGB(TITLE_FORE_R, TITLE_FORE_G, TITLE_FORE_B);
                    break;
                case Colors.Title_BackColor:
                    ret = EntityColor.createRGB(TITLE_BACK_R, TITLE_BACK_G, TITLE_BACK_B);
                    break;
                default:
                    ret = EntityColor.createRGB(DEFAULT, DEFAULT, DEFAULT);
                    break;
            }

            return ret;
        }
    }
}
