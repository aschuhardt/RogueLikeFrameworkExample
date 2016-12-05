using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike {
    [Flags]
    enum InputType {
        None = 0,
        Quit = 1,
        Up = 2,
        Down = 4,
        Left = 8,
        Right = 16,
        Enter = 32,
        Escape = 64
    }

    class InputFlagHelper {
        private InputFlagHelper() { }

        public static bool isInputFlagSet(InputType input, InputType inputToCheckFor) {
            return ((input & inputToCheckFor) != InputType.None);
        }

        public static void setFlag(ref InputType input, InputType inputToAdd) {
            input |= inputToAdd;
        }
    }
}
