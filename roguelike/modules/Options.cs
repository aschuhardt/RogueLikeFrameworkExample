using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike.modules {
    class Options : ModuleBase {
        protected override State getModuleState() {
            return State.Options;
        }

        protected override bool initModule(IList<object> parameters) {
            throw new NotImplementedException();
        }

        protected override void runModule() {
            throw new NotImplementedException();
        }
    }
}
