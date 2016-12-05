﻿namespace roguelike {
    class GlobalStatics {
        private GlobalStatics() { }
        public const int DEFAULT_WINDOW_WIDTH = 800;
        public const int DEFAULT_WINDOW_HEIGHT = 600;
        public const int FONT_HEIGHT = 16;
        public const int FONT_WIDTH = 9;
        public const int FONT_BACKGROUND_HEIGHT_ADJUSTMENT = 6;
        public const int FONT_BACKGROUND_VERT_POS_ADJUSTMENT = 4;
        public const string WINDOW_TITLE = "Testing";
        public const string FONT_PATH = "typeface/MorePerfectDOSVGA.ttf";
        public const int ANTIALIASING_LEVEL = 4;
        public const int BIT_DEPTH = 24;
        public const int STENCIL_DEPTH = 0;
        public const string LINE_DELIMITER = "%endline%";
        public const string TITLE_ART =    
            "                   ###                             " + LINE_DELIMITER
          + "  ####################                             " + LINE_DELIMITER
          + " #####################                             " + LINE_DELIMITER
          + "   ###################                             " + LINE_DELIMITER
          + "         #####                                     " + LINE_DELIMITER
          + "         #####                                     " + LINE_DELIMITER
          + "         #####    #######     ######  ############ " + LINE_DELIMITER
          + "         #####   ##     ##   ###      ############ " + LINE_DELIMITER
          + "          ###   ##       #  ###            ##    # " + LINE_DELIMITER
          + "          ####  ##########  #######        ##      " + LINE_DELIMITER
          + "         #####  #######        ######      ##      " + LINE_DELIMITER
          + "         #####  ###    ##          ##      ##      " + LINE_DELIMITER
          + "         #####  ##########  # ####  #      ##      " + LINE_DELIMITER
          + "          ####    #######   #########    ####      ";
        public const string ABOUT_TEXT =
              "About:" + LINE_DELIMITER
            + "This is a test of a text-based game framework developed using SFML." + LINE_DELIMITER
            + "See https://github.com/aschuhardt for information and source.";

        
    }
}