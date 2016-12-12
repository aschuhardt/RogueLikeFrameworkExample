using System.Collections.Generic;
using System.Linq;
using RoguePanda.entity;
using RoguePanda.entity.entitycolor;

namespace RoguePanda.modules {
    public abstract class ModuleBase : IModule {
        private const float ENTITY_LAYER_STATIC = float.MinValue;
        private const float ENTITY_LAYER_NORMAL = 0.0f;
        private IList<IDrawObject> _entities;
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

        public IEnumerable<IDrawObject> entities {
            get {
                return _entities;
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
            _entities = new List<IDrawObject>();
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

        protected void addEntity(string content, Colors foreColor, Colors backColor, float x = 0.0f, float y = 0.0f, bool isStatic = false) {
            DrawObjectColor mappedForeColor = ColorMapper.getColor(foreColor);
            DrawObjectColor mappedBackColor = ColorMapper.getColor(backColor);
            addEntity(content, mappedForeColor, mappedBackColor, x, y, isStatic);
        }

        protected void addEntity(string content, DrawObjectColor foreColor, DrawObjectColor backColor, float x = 0.0f, float y = 0.0f, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            IDrawObject newEnt = new FlexibleEntity(content, foreColor, backColor, x, y, layer);
            _entities.Add(newEnt);
        }

        protected void drawBorders(bool isStatic = true) {
            DrawObjectColor mappedForeColor = ColorMapper.getColor(Colors.Border_ForeColor);
            DrawObjectColor mappedBackColor = ColorMapper.getColor(Colors.Border_BackColor);
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (IDrawObject ent in DrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, mappedForeColor, mappedBackColor, layer)) {
                _entities.Add(ent);
            }
        }

        protected void drawRect(string content, Colors foreColor, Colors backColor, float x1, float y1, float x2, float y2, bool isStatic = false) {
            DrawObjectColor mappedForeColor = ColorMapper.getColor(foreColor);
            DrawObjectColor mappedBackColor = ColorMapper.getColor(backColor);
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (IDrawObject ent in DrawingMacros.drawRect(content, mappedForeColor, mappedBackColor, x1, y1, x2, y2, layer)) {
                _entities.Add(ent);
            }
        }

        protected void clearEntities(bool preserveStatics = true) {
            if (preserveStatics) {
                IList<IDrawObject> staticEntities = new List<IDrawObject>(_entities.Where((x) => x.layer == ENTITY_LAYER_STATIC));
                _entities.Clear();
                foreach (IDrawObject ent in staticEntities) {
                    _entities.Add(ent);
                }
            } else {
                _entities.Clear();
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
