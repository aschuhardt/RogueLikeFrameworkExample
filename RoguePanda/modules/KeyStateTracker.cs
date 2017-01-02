using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.modules {
    internal class KeyStateTracker {
        private IDictionary<InputType, KeyState> _inputStatuses;
        
        public KeyStateTracker() {
            _inputStatuses = new Dictionary<InputType, KeyState>();
            foreach (InputType t in Enum.GetValues(typeof(InputType))) {
                _inputStatuses.Add(t, new KeyState());
            }
        }

        public void run(InputType newInput) {
            foreach (InputType t in _inputStatuses.Keys) {
                KeyState ks = _inputStatuses[t];
                if (InputFlagHelper.isInputFlagSet(newInput, t)) {
                    if (ks.isNew) {
                        ks.isNew = false;
                        ks.isActive = true;
                        ks.ticksRemaining = KeyState.COOLDOWN_TICKS_INITIAL;
                    } else {
                        ks.ticksRemaining += KeyState.COOLDOWN_TICKS_SECONDARY;
                    }
                } else {
                    if (ks.ticksRemaining == 0) {
                        if (!ks.isNew) {
                            ks.isNew = true;
                            ks.isActive = false;
                        }
                    } else {
                        ks.ticksRemaining--;
                    }
                }
            }
        }

        public bool isKeyDown(InputType t) {
            return _inputStatuses[t].isActive;
        }
    }
}
