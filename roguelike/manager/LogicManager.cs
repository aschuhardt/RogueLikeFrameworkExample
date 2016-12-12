using System;
using RoguePanda.entity;
using RoguePanda.modules;
using System.Collections.Generic;

namespace RoguePanda.manager {
    class LogicManager : ManagerBase {
        private string DEFAULT_STATE = ConfigManager.Instance.Configuration.DefaultModule;

        private IList<IDrawObject> _entities;
        private IModule _currentModule;

        public InputType currentInput { get; set; }
        public string keyPressed { get; set; }
        public bool shouldReInitializeWindow { get; private set; }
        public VideoSettings videoSettings { get; set; }

        public LogicManager() {
            _entities = new List<IDrawObject>();
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
                nextState.videoSettings = videoSettings;
                if (nextState.init(_currentModule.transferParameters)) {
                    _currentModule = nextState;
                } else {
                    string nextStateString = _currentModule.nextStateType.ToString();
                    Console.WriteLine($"Failed to initiate module {nextStateString}.");
                }
            }

            //set state input
            _currentModule.setInput(currentInput);
            _currentModule.keyPressed = keyPressed;

            //run the module's logic
            _currentModule.run();

            //store whether the modules wants the window to reinitialize (i.e. changing video settings)
            shouldReInitializeWindow = _currentModule.shouldReInitializeWindow;

            //wait until the window has a chance to reinitialize before sending the entities for drawing
            if (!shouldReInitializeWindow) {
                //cache entities created by module
                foreach (IDrawObject ent in _currentModule.entities) _entities.Add(ent);
            } else {
                videoSettings = _currentModule.videoSettings;
                //reinitialize window with new settings
                //Note: make sure that whatever is being reinitialized here doesn't reset its own parameters in init()
                _currentModule.init(null);
            }
        }

        public IEnumerable<IDrawObject> getEntities() {
            return _entities;
        }

    }
}
