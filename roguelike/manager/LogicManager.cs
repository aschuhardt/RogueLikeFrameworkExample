using System;
using roguelike.entity;
using roguelike.modules;
using System.Collections.Generic;

namespace roguelike.manager {
    class LogicManager : ManagerBase {
        private const State DEFAULT_STATE = State.MainMenu;

        private IList<IEntity> _entities;
        private IModule _currentModule;

        public InputType currentInput { private get; set; }
        public bool shouldReInitializeWindow { get; private set; }
        public VideoSettings videoSettings { get; set; }

        public LogicManager() {
            _entities = new List<IEntity>();
        }

        public bool init() {
            //start at MainMenu state
            _currentModule = StateMapper.TransitToState(DEFAULT_STATE);
            _currentModule.videoSettings = videoSettings ?? null;
            _currentModule.init(null);
            return true;
        }

        public override void run() {
            //reset window-reinit flag
            shouldReInitializeWindow = false;

            //clear entity buffer
            _entities.Clear();

            //transit states if needed
            if (_currentModule.closing) {
                IModule nextState = StateMapper.TransitToState(_currentModule.nextStateType);
                if (nextState.init(_currentModule.transferParameters)) {
                    _currentModule = nextState;
                } else {
                    string nextStateString = _currentModule.nextStateType.ToString();
                    Console.WriteLine($"Failed to initiate module {nextStateString}.");
                }
            }

            //set state input
            _currentModule.setInput(currentInput);

            //run the module's logic
            _currentModule.run();

            //store whether the modules wants the window to reinitialize (i.e. changing video settings)
            shouldReInitializeWindow = _currentModule.shouldReInitializeWindow;

            //wait until the window has a chance to reinitialize before sending the entities for drawing
            if (!shouldReInitializeWindow) {
                //cache entities created by module
                foreach (IEntity ent in _currentModule.entities) _entities.Add(ent);
            } else {
                videoSettings = _currentModule.videoSettings;
            }
        }

        public IEnumerable<IEntity> getEntities() {
            return _entities;
        }

    }
}
