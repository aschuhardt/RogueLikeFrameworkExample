using System.Collections.Generic;
using roguelike.entity;

namespace roguelike.modules {
    abstract class ModuleBase : IModule {
        protected IList<IEntity> _entities;
        protected uint _windowWidth;
        protected uint _windowHeight;
        protected string _keyPressed;
        private IList<object> _transferParams;
        private InputType _input;
        private bool _closing;
        private State _nextState;
        private bool _reinitWindow;
        private VideoSettings _videoSettings;

        public bool closing {
            get {
                return _closing;
            }
        }

        public IEnumerable<IEntity> entities {
            get {
                return _entities;
            }
        }

        public State moduleState {
            get {
                return getModuleState();
            }
        }

        public State nextStateType {
            get {
                return _nextState;
            }
        }

        public IList<object> transferParameters {
            get {
                return _transferParams;
            }
        }

        public bool shouldReInitializeWindow {
            get {
                //reset to false after retrieving for safety's sake
                bool result = _reinitWindow;
                _reinitWindow = false;
                return result;
            }
        }
        
        VideoSettings IModule.videoSettings {
            get {
                return _videoSettings;
            }

            set {
                _videoSettings = value;
            }
        }

        public string keyPressed {
            get {
                return _keyPressed;
            }

            set {
                _keyPressed = value;
            }
        }

        public bool init(IList<object> parameters) {
            _windowWidth = _videoSettings.width;
            _windowHeight = _videoSettings.height;
            return initModule(parameters);
        }

        public void run() {
            runModule();
        }

        public ModuleBase() {
            _closing = false;
            _entities = new List<IEntity>();
            _transferParams = new List<object>();
            _reinitWindow = false;
            _videoSettings = null;
        }

        protected void resizeWindow(uint width, uint height, uint aalevel = 0, bool fullscrn = false) {
            _videoSettings = new VideoSettings() {
                width = width,
                height = height,
                aalevel = aalevel,
                fullscreen = fullscrn
            };
            _windowWidth = width;
            _windowHeight = height;
            _reinitWindow = true;
        }

        protected void transitionToState(State nextState, IList<object> parameters = null) {
            _closing = true;
            _transferParams = parameters;
            _nextState = nextState;
        }

        protected bool testInput(InputType inType) {
            return InputFlagHelper.isInputFlagSet(_input, inType);
        }

        protected abstract State getModuleState();
        protected abstract bool initModule(IList<object> parameters);
        protected abstract void runModule();

        public void setInput(InputType input) {
            _input = input;
        }
    }
}
