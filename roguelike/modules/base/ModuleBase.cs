using System;
using System.Collections.Generic;
using roguelike.entity;

namespace roguelike.modules {
    abstract class ModuleBase : IModule {
        protected List<IEntity> _entities;
        protected List<object> _transferParams;
        protected bool _closing;
        protected State _nextState;
        protected InputType _input;
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

        public bool init(IList<object> parameters) {
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
            _reinitWindow = true;
        }

        protected abstract State getModuleState();
        protected abstract bool initModule(IList<object> parameters);
        protected abstract void runModule();

        public void setInput(InputType input) {
            _input = input;
        }
    }
}
