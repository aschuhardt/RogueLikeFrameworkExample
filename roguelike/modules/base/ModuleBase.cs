using System;
using System.Collections.Generic;
using roguelike.entity;

namespace roguelike.modules {
    abstract class ModuleBase : IState {
        protected List<IEntity> _entities;
        protected List<object> _transferParams;
        protected bool _closing;
        protected State _nextState;
        protected InputType _input;

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
        }

        protected abstract State getModuleState();
        protected abstract bool initModule(IList<object> parameters);
        protected abstract void runModule();

        public void setInput(InputType input) {
            _input = input;
        }
    }
}
