using System;
using System.Collections.Generic;
using roguelike.entity;
using roguelike.entity.entitycolor;
using roguelike.manager;
using SFML.Graphics;

namespace roguelike.entity {
    class EntityDrawingMacros {
        private const char BORDER_CHARACTER = '#';

        private EntityDrawingMacros() { }

        public static IEntity[] drawRect(char glyph, EntityColor foreColor, EntityColor backColor, float x1, float y1, float x2, float y2, float layer = 0.0f) {
            List<IEntity> result = new List<IEntity>();

            int tileWidth = GlobalStatics.FONT_WIDTH;
            int tileHeight = GlobalStatics.FONT_HEIGHT;

            for (int x = Convert.ToInt32(x1 / tileWidth); x <= x2 / tileWidth; x++) {
                for (int y = Convert.ToInt32(y1 / tileHeight); y <= y2 / tileHeight; y++) {
                    FlexibleEntity ent = new FlexibleEntity(glyph.ToString(), foreColor, backColor, x * tileWidth, y * tileHeight, layer);
                    result.Add(ent);
                }
            }

            return result.ToArray();
        }

        public static IEntity[] drawWindowBorders(EntityColor borderForeColor, EntityColor borderBackColor, float layer = 0.0f) {
            List<IEntity> result = new List<IEntity>();
            int height = GlobalStatics.DEFAULT_WINDOW_HEIGHT;
            int width = GlobalStatics.DEFAULT_WINDOW_WIDTH;
            int fontHeight = GlobalStatics.FONT_HEIGHT;
            int fontWidth = GlobalStatics.FONT_WIDTH;

            int top = 0;
            int bottom = height - fontHeight;
            int left = 0;
            int right = width - fontWidth;

            //top
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, left, top, right, top, layer));

            //bottom
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, left, bottom, right, bottom, layer));

            //left
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, left, top + 1, left, bottom, layer));

            //right
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, right, top + 1, right, bottom));
            
            return result.ToArray();
        }

        public static string[] splitMultiLineDelimited(string input) {
            string[] lineDelimiters = new string[] { GlobalStatics.LINE_DELIMITER };
            string[] lines = input.Split(lineDelimiters, StringSplitOptions.None);
            return lines;
        }
    }
}
