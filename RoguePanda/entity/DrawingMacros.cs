using System;
using System.Collections.Generic;
using RoguePanda.entity;
using RoguePanda.entity.color;
using RoguePanda.manager;
using SFML.Graphics;

namespace RoguePanda.entity {
    class DrawingMacros {
        private const string BORDER_CHARACTER = "#";

        private DrawingMacros() { }

        public static ITextEntity[] drawRect(string glyph, EntityColor foreColor, EntityColor backColor, float x1, float y1, float x2, float y2, float layer = 0.0f) {
            List<ITextEntity> result = new List<ITextEntity>();

            int tileWidth = ConfigManager.Config.FontWidth;
            int tileHeight = ConfigManager.Config.FontHeight;

            for (int x = Convert.ToInt32(x1 / tileWidth); x <= Math.Round(x2 / tileWidth); x++) {
                for (int y = Convert.ToInt32(y1 / tileHeight); y <= Math.Round(y2 / tileHeight); y++) {
                    SimpleTextEntity ent = new SimpleTextEntity(glyph, foreColor, backColor, x * tileWidth, y * tileHeight, layer);
                    result.Add(ent);
                }
            }

            return result.ToArray();
        }

        public static ITextEntity[] drawWindowBorders(uint width, uint height, EntityColor borderForeColor, EntityColor borderBackColor, float layer = 0.0f) {
            List<ITextEntity> result = new List<ITextEntity>();
            int fontHeight = ConfigManager.Config.FontHeight;
            int fontWidth = ConfigManager.Config.FontWidth;

            int top = 0;
            int bottom = Convert.ToInt32(height - fontHeight);
            int left = 0;
            int right = Convert.ToInt32(width - fontWidth);

            //top
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, left, top, right, top, layer));

            //bottom
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, left, bottom, right, bottom, layer));

            //left
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, left, top + 1, left, bottom, layer));

            //right
            result.AddRange(drawRect(BORDER_CHARACTER, borderForeColor, borderBackColor, right, top + 1, right, bottom, layer));
            
            return result.ToArray();
        }

        public static string[] splitMultiLineDelimited(string input) {
            string[] lineDelimiters = new string[] { ConfigManager.Config.LineDelimiter };
            string[] lines = input.Split(lineDelimiters, StringSplitOptions.None);
            return lines;
        }
    }
}
