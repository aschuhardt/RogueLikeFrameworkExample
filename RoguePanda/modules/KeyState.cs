using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.modules {
    internal class KeyState {
        public const int COOLDOWN_TICKS_INITIAL = 30;
        public const int COOLDOWN_TICKS_SECONDARY = 1;

        public int ticksRemaining { get; set; }
        public bool isNew { get; set; }
        public bool isActive { get; set; }

        public KeyState() {
            ticksRemaining = 0;
            isNew = true;
            isActive = false;
        }
    }
}
