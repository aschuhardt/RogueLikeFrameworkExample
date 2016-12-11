using System;

namespace RoguePanda {
    /// <summary>
    /// Abstractions of various types of user-inputs that can be detected by InputManager and processed by modules.
    /// Provides a means by which to keep modules separate from the SFML binaries.
    /// </summary>
    [Flags]
    enum InputType {
        None = 0,
        Quit = 1,
        Up = 2,
        Down = 4,
        Left = 8,
        Right = 16,
        Enter = 32,
        Escape = 64,
        BackSpace = 128
    }

    /// <summary>
    /// A simple helper for performing some routine tasks with the InputType enum.
    /// </summary>
    class InputFlagHelper {
        private InputFlagHelper() { }

        /// <summary>
        /// Checks whether the given InputType union has a particular flag set.
        /// </summary>
        /// <param name="input">The InputType union that is meant to be checked for flags.</param>
        /// <param name="inputToCheckFor">The flag that is being checked for in the given union.</param>
        /// <returns>True if the flag is set in the given union, otherwise false.</returns>
        public static bool isInputFlagSet(InputType input, InputType inputToCheckFor) {
            return ((input & inputToCheckFor) != InputType.None);
        }

        /// <summary>
        /// Sets a flag to the logical-true state in the given InputType union.
        /// </summary>
        /// <param name="input">The union that the action is meant to be applied to.</param>
        /// <param name="inputToAdd">The flag that is meant to be set in the given union.</param>
        public static void setFlag(ref InputType input, InputType inputToAdd) {
            input |= inputToAdd;
        }
    }
}
