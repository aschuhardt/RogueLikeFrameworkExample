using System.Collections.Generic;
using roguelike.entity;
using roguelike.entity.entitycolor;

namespace roguelike.modules {
    class About : ModuleBase {
        private const float TEXT_X = 64;
        private const float TEXT_Y = 64;

        protected override State getModuleState() {
            return State.About;
        }

        protected override bool initModule(IList<object> parameters) {
            //draw borders
            EntityColor borderForeColor = EntityColor.createRGB(255, 102, 0);
            EntityColor borderBackColor = EntityColor.createRGB(0, 0, 0);
            foreach (IEntity ent in EntityDrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, borderForeColor, borderBackColor)) {
                _entities.Add(ent);
            }

            //draw about text
            string[] aboutText = EntityDrawingMacros.splitMultiLineDelimited(GlobalStatics.ABOUT_TEXT);
            int lineOffset = 0;
            EntityColor aboutTextForeColor = EntityColor.createRGB(16, 75, 169);
            EntityColor aboutTextBackColor = EntityColor.createRGB(0, 0, 0);
            foreach (string line in aboutText) {
                IEntity lineEnt = new FlexibleEntity(line, aboutTextForeColor, aboutTextBackColor, TEXT_X, TEXT_Y + lineOffset);
                lineOffset += GlobalStatics.FONT_HEIGHT;
                _entities.Add(lineEnt);
            }
            return true;
        }

        protected override void runModule() {
            if (testInput(InputType.Escape)) {
                transitionToState(State.MainMenu);
            }
        }
    }
}
