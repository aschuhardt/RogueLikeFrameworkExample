using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.manager {
    abstract class ManagerBase {
        public string errorMessage { get; protected set; }
        public abstract void run();
    }
}
