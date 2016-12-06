using System;
using System.Collections.Generic;

namespace roguelike.modules {
    class Play : ModuleBase {
        private string _playerName = "";

        protected override State getModuleState() {
            return State.Play;
        }

        protected override bool initModule(IList<object> parameters) {
            throw new NotImplementedException();
        }

        protected override void runModule() {
            throw new NotImplementedException();
        }
    }
}
