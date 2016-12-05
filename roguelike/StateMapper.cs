using roguelike.modules;

namespace roguelike {
    class StateMapper {
        private StateMapper() { }

        public static IState TransitToState(State stateToEnter) {
            IState output;
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
                default:
                    output = null;
                    break;
            }

            return output;
        }
    }
}
