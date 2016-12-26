using System;
using System.Linq;
using RoguePanda.entity;
using RoguePanda.modules;
using System.Collections.Generic;

namespace RoguePanda.manager {
    internal class LogicManager : ManagerBase {
        private string DEFAULT_STATE = ConfigManager.Config.DefaultModule;

        private IList<ITextEntity> _textEntities;
        private IList<ISpriteEntity> _spriteEntities;
        private IList<IAudioEntity> _audioEntities;
        private IList<string> _requestedAudioStopTags;
        private IModule _currentModule;

        public InputType currentInput { get; set; }
        public string keyPressed { get; set; }
        public bool shouldReInitializeWindow { get; private set; }
        public VideoSettings videoSettings { get; set; }

        public LogicManager() {
            _textEntities = new List<ITextEntity>();
            _spriteEntities = new List<ISpriteEntity>();
            _audioEntities = new List<IAudioEntity>();
            _requestedAudioStopTags = new List<string>();
        }

        public override bool init() {
            //start at MainMenu state
            _currentModule = StateMapper.TransitToState(DEFAULT_STATE);
            _currentModule.videoSettings = videoSettings ?? null;
            _currentModule.init(null);
            return true;
        }

        public override void run() {
            //reset window-reinit flag
            shouldReInitializeWindow = false;

            //clear entity buffers
            _textEntities.Clear();
            _spriteEntities.Clear();
            _audioEntities.Clear();
            _requestedAudioStopTags.Clear();

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
                foreach (ITextEntity ent in _currentModule.textEntities) _textEntities.Add(ent);
                foreach (ISpriteEntity ent in _currentModule.spriteEntities) _spriteEntities.Add(ent);
                foreach (IAudioEntity ent in _currentModule.audioEntities) _audioEntities.Add(ent);
                foreach (string tag in _currentModule.requestedAudioStopTags) _requestedAudioStopTags.Add(tag);
            } else {
                videoSettings = _currentModule.videoSettings;
                //reinitialize window with new settings
                //Note: make sure that whatever is being reinitialized here doesn't reset its own parameters in init()
                _currentModule.init(null);
            }
        }

        public IEnumerable<ITextEntity> textEntities {
            get {
                return _textEntities;
            }
        }

        public IEnumerable<ISpriteEntity> spriteEntities {
            get {
                return _spriteEntities;
            }
        }

        public IEnumerable<IAudioEntity> audioEntities {
            get {
                return _audioEntities;
            }
        }

        public IEnumerable<string> requestedAudioStopTags {
            get {
                return _requestedAudioStopTags;
            }
        }
    }
}
