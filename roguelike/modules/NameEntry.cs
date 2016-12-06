using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike.modules {
    class NameEntry : ModuleBase {
        string _playerName;

        protected override State getModuleState() {
            return State.NameEntry;
        }

        protected override bool initModule(IList<object> parameters) {
            _playerName = "";

            return true;
        }

        protected override void runModule() {
            
        }
    }
}
