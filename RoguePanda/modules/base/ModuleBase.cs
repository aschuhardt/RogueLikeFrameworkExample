using System;
using System.Collections.Generic;
using System.Linq;
using RoguePanda.entity;
using RoguePanda.entity.color;

namespace RoguePanda.modules {
    /// <summary>
    /// Base object from which all developer-defined modules will derive.
    /// Contains information relevent to the current module's state, as well
    /// as providing some abstraction of routine processes that modules will perform.
    /// </summary>
    public abstract class ModuleBase : IModule {
        //constants
        private const float ENTITY_LAYER_STATIC = float.MinValue;
        private const float ENTITY_LAYER_NORMAL = 0.0f;
        
        //containers
        private IList<ITextEntity> _textObjects;
        private IList<ISpriteEntity> _spriteObjects;
        private IList<IAudioEntity> _audioEntities;
        private IList<string> _requestedAudioStopTags; //used to store a collection of audio entity tags that the derivative would like to close
        private IList<object> _transferParams;

        //flow control
        private InputType _input;
        private bool _closing;
        private string _nextState;
        private bool _reinitWindow;
        private KeyStateTracker _keyStatesTracker;

        //misc members
        private VideoSettings _videoSettings; //stores current screen setting information
        private bool _firstRun; //this is used to bypass clearing the audio buffer immediately after initialization, 
                                //  so that it's possible to start up a track on module init without it immediately being disposed of.
        private EntityColor _defaultTextForecolor;
        private EntityColor _defaultTextBackcolor;

        /// <summary>
        /// The current width of the game window.
        /// </summary>
        protected uint _windowWidth;

        /// <summary>
        /// The current height of the game window.
        /// </summary>
        protected uint _windowHeight;

        /// <summary>
        /// Text representation of currently-pressed key.
        /// Only usable for text/typing input, not for fluid control.
        /// </summary>
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

        /// <summary>
        /// Objects that can be passed between modules when the engine is forced to change state.
        /// </summary>
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
            _defaultTextForecolor = EntityColor.createRGB(255, 255, 255);
            _defaultTextBackcolor = EntityColor.createRGB(0, 0, 0);
            _keyStatesTracker = new KeyStateTracker();
            return initModule(parameters);
        }

        public void run() {
            if (!_firstRun) {
                _audioEntities.Clear();
                _requestedAudioStopTags.Clear();
            } else {
                _firstRun = false;
            }
            //keep track of input states
            _keyStatesTracker.run(_input);

            //run module
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

        /// <summary>
        /// Use this method to force the game window to resize.
        /// </summary>
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

        /// <summary>
        /// Use this method to force the game engine to transition to a new module,
        /// and optionally to pass the new module a collection of objects.
        /// </summary>
        protected void transitionToState(string nextState, IList<object> parameters = null) {
            _closing = true;
            _transferParams = parameters;
            _nextState = nextState;
        }

        /// <summary>
        /// Detect whether a particular key was entered.
        /// Warning: Use "isKeyPressed" for more fluid UI behavior.
        /// </summary>
        /// <returns>True if the particular input was entered.</returns>
        protected bool testInput(InputType inType) {
            return InputFlagHelper.isInputFlagSet(_input, inType);
        }

        /// <summary>
        /// Used to draw text at a location on-screen.
        /// </summary>
        protected void drawText(string content, EntityColor? foreColor = null, EntityColor? backColor = null, float x = 0.0f, float y = 0.0f, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            ITextEntity newEnt = new SimpleTextEntity(content,
                (foreColor ?? _defaultTextForecolor),
                (backColor ?? _defaultTextBackcolor),
                x, 
                y, 
                layer);
            _textObjects.Add(newEnt);
        }

        /// <summary>
        /// Used to draw a sprite from among the loaded assets onto the window.
        /// </summary>
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

        /// <summary>
        /// Plays audio.  The entire asset will be loaded into memory before being played.
        /// </summary>
        protected void playSound(string assetName, float volume = 100.0f, float pitch = 1.0f, bool loop = false, string tag = "") {
            IAudioEntity newEnt = new SimpleAudioEntity(assetName, AudioEntityType.Sound, tag, volume, pitch, loop);
            _audioEntities.Add(newEnt);
        }

        /// <summary>
        /// Play audio.  The asset will be streamed from its location on-disk in order to save memory when dealing with larger files.
        /// </summary>
        protected void playMusic(string assetName, float volume = 100.0f, float pitch = 1.0f, bool loop = false, string tag = "") {
            IAudioEntity newEnt = new SimpleAudioEntity(assetName, AudioEntityType.Music, tag, volume, pitch, loop);
            _audioEntities.Add(newEnt);
        }

        /// <summary>
        /// Stops all playing audio that has the specified tag.
        /// </summary>
        protected void stopAudio(string tag) {
            _requestedAudioStopTags.Add(tag);
        }

        protected void drawBorders(EntityColor foreColor, EntityColor backColor, bool isStatic = true) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (ITextEntity ent in DrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, foreColor, backColor, layer)) {
                _textObjects.Add(ent);
            }
        }

        /// <summary>
        /// Used to detect whether a key is currently being pressed.  Use this for fluid UI interaction.
        /// </summary>
        /// <returns>True if the key is pressed, otherwise False.</returns>
        protected bool isKeyDown(InputType inputToTest) {
            return _keyStatesTracker.isKeyDown(inputToTest);
        }

        protected void drawRect(string content, EntityColor foreColor, EntityColor backColor, float x1, float y1, float x2, float y2, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (ITextEntity ent in DrawingMacros.drawRect(content, foreColor, backColor, x1, y1, x2, y2, layer)) {
                _textObjects.Add(ent);
            }
        }

        /// <summary>
        /// Clears drawn objects (text + sprites).  Optionally, also clears objects marked as Static (i.e. those that will not otherwise be cleared each frame).
        /// </summary>
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
