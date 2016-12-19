using System.Collections.Generic;
using System.Linq;
using RoguePanda.drawobject;
using RoguePanda.drawobject.color;

namespace RoguePanda.modules {
    public abstract class ModuleBase : IModule {
        private const float ENTITY_LAYER_STATIC = float.MinValue;
        private const float ENTITY_LAYER_NORMAL = 0.0f;
        private IList<IDrawObject> _drawObjects;
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

        public IEnumerable<IDrawObject> drawObjects {
            get {
                return _drawObjects;
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
            _drawObjects = new List<IDrawObject>();
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
        
        protected void addDrawObject(string content, DrawObjectColor foreColor, DrawObjectColor backColor, float x = 0.0f, float y = 0.0f, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            IDrawObject newEnt = new FlexibleEntity(content, foreColor, backColor, x, y, layer);
            _drawObjects.Add(newEnt);
        }

        protected void drawBorders(DrawObjectColor foreColor, DrawObjectColor backColor, bool isStatic = true) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (IDrawObject ent in DrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, foreColor, backColor, layer)) {
                _drawObjects.Add(ent);
            }
        }

        protected void drawRect(string content, DrawObjectColor foreColor, DrawObjectColor backColor, float x1, float y1, float x2, float y2, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (IDrawObject ent in DrawingMacros.drawRect(content, foreColor, backColor, x1, y1, x2, y2, layer)) {
                _drawObjects.Add(ent);
            }
        }

        protected void clearDrawObjects(bool preserveStatics = true) {
            if (preserveStatics) {
                IList<IDrawObject> staticEntities = new List<IDrawObject>(_drawObjects.Where((x) => x.layer == ENTITY_LAYER_STATIC));
                _drawObjects.Clear();
                foreach (IDrawObject ent in staticEntities) {
                    _drawObjects.Add(ent);
                }
            } else {
                _drawObjects.Clear();
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
