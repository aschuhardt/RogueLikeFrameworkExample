using System;
using System.Collections.Generic;
using System.Linq;
using RoguePanda.entity;
using RoguePanda.entity.color;

namespace RoguePanda.modules {
    public abstract class ModuleBase : IModule {
        private const float ENTITY_LAYER_STATIC = float.MinValue;
        private const float ENTITY_LAYER_NORMAL = 0.0f;
        private IList<ITextEntity> _textObjects;
        private IList<ISpriteEntity> _spriteObjects;
        private IList<IAudioEntity> _audioEntities;
        private IList<string> _requestedAudioStopTags;
        private IList<object> _transferParams;
        private InputType _input;
        private bool _closing;
        private string _nextState;
        private bool _reinitWindow;
        private VideoSettings _videoSettings;
        private bool _firstRun; //this is used to bypass clearing the audio buffer immediately after initialization, 
                                //so that it's possible to start up a track on module init without it immediately being disposed of.

        protected uint _windowWidth;
        protected uint _windowHeight;
        protected string _keyPressed;

        public bool closing {
            get {
                return _closing;
            }
        }

        public IEnumerable<ITextEntity> textEntities {
            get {
                return _textObjects;
            }
        }

        public IEnumerable<ISpriteEntity> spriteEntities {
            get {
                return _spriteObjects;
            }
        }

        public string moduleState {
            get {
                return getModuleState();
            }
        }

        public string nextStateType {
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

        public bool init(IList<object> parameters) {
            _windowWidth = _videoSettings.width;
            _windowHeight = _videoSettings.height;
            return initModule(parameters);
        }

        public void run() {
            if (!_firstRun) {
                _audioEntities.Clear();
                _requestedAudioStopTags.Clear();
            } else {
                _firstRun = false;
            }
            runModule();
        }

        public ModuleBase() {
            _closing = false;
            _textObjects = new List<ITextEntity>();
            _spriteObjects = new List<ISpriteEntity>();
            _audioEntities = new List<IAudioEntity>();
            _requestedAudioStopTags = new List<string>();
            _transferParams = new List<object>();
            _reinitWindow = false;
            _videoSettings = null;
            _firstRun = true;
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

        protected void transitionToState(string nextState, IList<object> parameters = null) {
            _closing = true;
            _transferParams = parameters;
            _nextState = nextState;
        }

        protected bool testInput(InputType inType) {
            return InputFlagHelper.isInputFlagSet(_input, inType);
        }

        protected void drawText(string content, EntityColor foreColor, EntityColor backColor, float x = 0.0f, float y = 0.0f, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            ITextEntity newEnt = new SimpleTextEntity(content, foreColor, backColor, x, y, layer);
            _textObjects.Add(newEnt);
        }

        protected void drawSprite(string assetName, int width, int height, bool isStatic, int x = 0, int y = 0, float rotation = 0.0f, float scaleX = 1.0f, float scaleY = 1.0f, byte alpha = 255, int textureX = 0, int textureY = 0) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            ISpriteEntity newEnt = new SimpleSpriteEntity(assetName, width, height, layer) {
                x = x,
                y = y,
                rotation = rotation,
                scaleX = scaleX,
                scaleY = scaleY,
                alpha = 255,
                texPosX = textureX,
                texPosY = textureY
            };
            _spriteObjects.Add(newEnt);
        }

        protected void playSound(string assetName, float volume = 100.0f, float pitch = 1.0f, bool loop = false, string tag = "") {
            IAudioEntity newEnt = new SimpleAudioEntity(assetName, AudioEntityType.Sound, tag, volume, pitch, loop);
            _audioEntities.Add(newEnt);
        }

        protected void playMusic(string assetName, float volume = 100.0f, float pitch = 1.0f, bool loop = false, string tag = "") {
            IAudioEntity newEnt = new SimpleAudioEntity(assetName, AudioEntityType.Music, tag, volume, pitch, loop);
            _audioEntities.Add(newEnt);
        }

        protected void stopAudio(string tag) {
            _requestedAudioStopTags.Add(tag);
        }

        protected void drawBorders(EntityColor foreColor, EntityColor backColor, bool isStatic = true) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (ITextEntity ent in DrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, foreColor, backColor, layer)) {
                _textObjects.Add(ent);
            }
        }

        protected void drawRect(string content, EntityColor foreColor, EntityColor backColor, float x1, float y1, float x2, float y2, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (ITextEntity ent in DrawingMacros.drawRect(content, foreColor, backColor, x1, y1, x2, y2, layer)) {
                _textObjects.Add(ent);
            }
        }

        protected void clearDrawObjects(bool preserveStatics = true) {
            if (preserveStatics) {
                IList<ITextEntity> staticTextEntities = new List<ITextEntity>(_textObjects.Where((x) => x.layer == ENTITY_LAYER_STATIC));
                IList<ISpriteEntity> staticSpriteEntities = new List<ISpriteEntity>(_spriteObjects.Where((x) => x.layer == ENTITY_LAYER_STATIC));
                _textObjects.Clear();
                _spriteObjects.Clear();
                foreach (ITextEntity ent in staticTextEntities) {
                    _textObjects.Add(ent);
                }
                foreach (ISpriteEntity ent in staticSpriteEntities) {
                    _spriteObjects.Add(ent);
                }
            } else {
                _textObjects.Clear();
                _spriteObjects.Clear();
            }
        }

        protected abstract string getModuleState();
        protected abstract bool initModule(IList<object> parameters);
        protected abstract void runModule();

        public void setInput(InputType input) {
            _input = input;
        }
    }
}
