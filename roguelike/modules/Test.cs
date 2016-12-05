using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike.modules {
    class Test : ModuleBase {
        protected override State getModuleState() {
            return State.Test;
        }

        protected override bool initModule(IList<object> parameters) {
            throw new NotImplementedException();
        }

        protected override void runModule() {
            throw new NotImplementedException();
        }
    }
}
