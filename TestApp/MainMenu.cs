using System.Collections.Generic;
using RoguePanda.drawobject.color;
using RoguePanda.modules;
using RoguePanda.manager;

namespace testmodule {
    class MainMenu : ModuleBase {
        private float dx = 1.0f;
        private float dy = 1.0f;
        private float x = 0.0f;
        private float y = 0.0f;
        private string text = "asdf";

        protected override string getModuleState() {
            return "testmodule.MainMenu";
        }

        protected override bool initModule(IList<object> parameters) {

            return true;
        }

        protected override void runModule() {
            clearDrawObjects();

            addDrawObject(text, DrawObjectColor.createRGB(255, 240, 255), DrawObjectColor.createRGB(0, 0, 0), x, y);

            x += dx;
            y += dy;

            if (x >= _windowWidth - (text.Length * ConfigManager.Instance.Configuration.FontWidth)
                || x <= 0) {
                dx = -dx;
            }

            if (y >= _windowHeight - (ConfigManager.Instance.Configuration.FontHeight)
                || y <= 0) {
                dy = -dy;
            }
        }
    }
}
