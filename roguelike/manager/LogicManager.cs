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
        public bool shouldReInitializeWindow { get; private set; }
        public VideoSettings videoSettings { get; set; }

        public LogicManager() {
            _entities = new List<IEntity>();
        }

        public bool init() {
            //start at MainMenu state
            _currentState = StateMapper.TransitToState(DEFAULT_STATE);
            _currentState.videoSettings = videoSettings ?? null;
            _currentState.init(null);
            return true;
        }

        public override void run() {
            //reset window-reinit flag
            shouldReInitializeWindow = false;

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

            //store whether the modules wants the window to reinitialize (i.e. changing video settings)
            shouldReInitializeWindow = _currentState.shouldReInitializeWindow;

            //wait until the window has a chance to reinitialize before sending the entities for drawing
            if (!shouldReInitializeWindow) {
                //cache entities created by module
                foreach (IEntity ent in _currentState.entities) _entities.Add(ent);
            } else {
                videoSettings = _currentState.videoSettings;
            }
        }

        public IEnumerable<IEntity> getEntities() {
            return _entities;
        }

    }
}
