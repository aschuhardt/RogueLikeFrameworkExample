using roguelike.modules;

namespace roguelike {
    /// <summary>
    /// Provides a mapping between members of the State enum and implementations of the IModule interface.
    /// </summary>
    class StateMapper {
        private StateMapper() { }

        /// <summary>
        /// Returns an implementation of the IModule interface based on the given State.
        /// </summary>
        /// <param name="stateToEnter">The member of the State enum representing the desired IModule implementation.</param>
        /// <returns>An implementation of IModule corresponding to the given State.</returns>
        public static IModule TransitToState(State stateToEnter) {
            IModule output;
            switch (stateToEnter) {
                case State.MainMenu:
                    output = new MainMenu();
                    break;
                case State.Test:
                    output = new Test();
                    break;
                case State.Options:
                    output = new Options();
                    break;
                case State.About:
                    output = new About();
                    break;
                case State.Play:
                    output = new Play();
                    break;
                case State.NameEntry:
                    output = new NameEntry();
                    break;
                default:
                    output = null;
                    break;
            }

            return output;
        }
    }
}
