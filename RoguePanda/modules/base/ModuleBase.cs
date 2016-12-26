using System;
using System.Collections.Generic;
using System.Linq;
using RoguePanda.entity;
using RoguePanda.entity.color;

namespace RoguePanda.modules {
    public abstract class ModuleBase : IModule {
        private const float ENTITY_LAYER_STATIC = float.MinValue;
        private const float ENTITY_LAYER_NORMAL = 0.0f;
        private IList<ITextObject> _textObjects;
        private IList<ISpriteObject> _spriteObjects;
        private IList<object> _transferParams;
        private InputType _input;
        private bool _closing;
        private string _nextState;
        private bool _reinitWindow;
        private VideoSettings _videoSettings;

        protected uint _windowWidth;
        protected uint _windowHeight;
        protected string _keyPressed;

        public bool closing {
            get {
                return _closing;
            }
        }

        public IEnumerable<ITextObject> textObjects {
            get {
                return _textObjects;
            }
        }

        public IEnumerable<ISpriteObject> spriteObjects {
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
            _textObjects = new List<ITextObject>();
            _spriteObjects = new List<ISpriteObject>();
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

        protected void transitionToState(string nextState, IList<object> parameters = null) {
            _closing = true;
            _transferParams = parameters;
            _nextState = nextState;
        }

        protected bool testInput(InputType inType) {
            return InputFlagHelper.isInputFlagSet(_input, inType);
        }
        
        protected void addTextObject(string content, EntityColor foreColor, EntityColor backColor, float x = 0.0f, float y = 0.0f, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            ITextObject newEnt = new FlexibleTextObject(content, foreColor, backColor, x, y, layer);
            _textObjects.Add(newEnt);
        }

        protected void drawBorders(EntityColor foreColor, EntityColor backColor, bool isStatic = true) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (ITextObject ent in DrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, foreColor, backColor, layer)) {
                _textObjects.Add(ent);
            }
        }

        protected void drawRect(string content, EntityColor foreColor, EntityColor backColor, float x1, float y1, float x2, float y2, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (ITextObject ent in DrawingMacros.drawRect(content, foreColor, backColor, x1, y1, x2, y2, layer)) {
                _textObjects.Add(ent);
            }
        }

        protected void clearDrawObjects(bool preserveStatics = true) {
            if (preserveStatics) {
                IList<ITextObject> staticTextEntities = new List<ITextObject>(_textObjects.Where((x) => x.layer == ENTITY_LAYER_STATIC));
                IList<ISpriteObject> staticSpriteEntities = new List<ISpriteObject>(_spriteObjects.Where((x) => x.layer == ENTITY_LAYER_STATIC));
                _textObjects.Clear();
                _spriteObjects.Clear();
                foreach (ITextObject ent in staticTextEntities) {
                    _textObjects.Add(ent);
                }
                foreach (ISpriteObject ent in staticSpriteEntities) {
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
