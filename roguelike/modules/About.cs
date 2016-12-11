using System.Collections.Generic;
using roguelike.entity;
using roguelike.entity.entitycolor;

namespace roguelike.modules {
    class About : ModuleBase {
        private const float TEXT_X = 64;
        private const float TEXT_Y = 64;

        protected override string getModuleState() {
            return "roguelike.modules.About";
        }

        protected override bool initModule(IList<object> parameters) {
            //draw borders
            drawBorders();

            //draw about text
            string[] aboutText = EntityDrawingMacros.splitMultiLineDelimited(GlobalStatics.ABOUT_TEXT);
            int lineOffset = 0;
            foreach (string line in aboutText) {
                addEntity(line, Colors.LightText_ForeColor, Colors.LightText_BackColor, TEXT_X, TEXT_Y + lineOffset);
                lineOffset += GlobalStatics.FONT_HEIGHT;
            }
            return true;
        }

        protected override void runModule() {
            if (testInput(InputType.Escape)) {
                transitionToState("roguelike.modules.MainMenu");
            }
        }
    }
}
