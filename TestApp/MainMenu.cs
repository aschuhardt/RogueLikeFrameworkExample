using System.Collections.Generic;
using RoguePanda.modules;


namespace testmodule {
    class MainMenu : ModuleBase {
        protected override string getModuleState() {
            return "testmodule.MainMenu";
        }

        protected override bool initModule(IList<object> parameters) {
            addEntity("asdf", Colors.LightText_ForeColor, Colors.LightText_BackColor, isStatic:true);

            return true;
        }

        protected override void runModule() {
            //
        }
    }
}
