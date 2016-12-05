using System;
using roguelike.entity;
using roguelike.modules;
using System.Collections.Generic;

namespace roguelike.manager {
    class LogicManager : ManagerBase {
        private const State DEFAULT_STATE = State.MainMenu;

        private IList<IEntity> _entities;
        private IState _currentState;

        public InputType currentInput { private get; set; }

        public LogicManager() {
            _entities = new List<IEntity>();
        }

        public bool init() {
            //start at MainMenu state
            _currentState = StateMapper.TransitToState(DEFAULT_STATE);
            _currentState.init(null);
            return true;
        }

        public override void run() {
            //clear entity buffer
            _entities.Clear();

            //transit states if needed
            if (_currentState.closing) {
                IState nextState = StateMapper.TransitToState(_currentState.nextStateType);
                if (nextState.init(_currentState.transferParameters)) {
                    _currentState = nextState;
                } else {
                    string nextStateString = _currentState.nextStateType.ToString();
                    Console.WriteLine($"Failed to initiate module {nextStateString}.");                    
                }                
            }

            //set state input
            _currentState.setInput(currentInput);

            //run the module's logic
            _currentState.run();

            //cache entities created by module
            foreach (IEntity ent in _currentState.entities) _entities.Add(ent);
        }

        public IEnumerable<IEntity> getEntities() {
            return _entities;
        }
        
    }
}
