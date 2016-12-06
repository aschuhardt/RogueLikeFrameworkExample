using roguelike.modules;

namespace roguelike {
    class StateMapper {
        private StateMapper() { }

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
