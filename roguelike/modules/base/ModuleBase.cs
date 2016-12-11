using System.Collections.Generic;
using System.Linq;
using roguelike.entity;
using roguelike.entity.entitycolor;

namespace roguelike.modules {
    abstract class ModuleBase : IModule {
        private const float ENTITY_LAYER_STATIC = float.MinValue;
        private const float ENTITY_LAYER_NORMAL = 0.0f;
        private IList<IEntity> _entities;
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

        public IEnumerable<IEntity> entities {
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
            _entities = new List<IEntity>();
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
            EntityColor mappedForeColor = ColorMapper.getColor(foreColor);
            EntityColor mappedBackColor = ColorMapper.getColor(backColor);
            addEntity(content, mappedForeColor, mappedBackColor, x, y, isStatic);
        }

        protected void addEntity(string content, EntityColor foreColor, EntityColor backColor, float x = 0.0f, float y = 0.0f, bool isStatic = false) {
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            IEntity newEnt = new FlexibleEntity(content, foreColor, backColor, x, y, layer);
            _entities.Add(newEnt);
        }

        protected void drawBorders(bool isStatic = true) {
            EntityColor mappedForeColor = ColorMapper.getColor(Colors.Border_ForeColor);
            EntityColor mappedBackColor = ColorMapper.getColor(Colors.Border_BackColor);
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (IEntity ent in EntityDrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, mappedForeColor, mappedBackColor, layer)) {
                _entities.Add(ent);
            }
        }

        protected void drawRect(string content, Colors foreColor, Colors backColor, float x1, float y1, float x2, float y2, bool isStatic = false) {
            EntityColor mappedForeColor = ColorMapper.getColor(foreColor);
            EntityColor mappedBackColor = ColorMapper.getColor(backColor);
            float layer = isStatic ? ENTITY_LAYER_STATIC : ENTITY_LAYER_NORMAL;
            foreach (IEntity ent in EntityDrawingMacros.drawRect(content, mappedForeColor, mappedBackColor, x1, y1, x2, y2, layer)) {
                _entities.Add(ent);
            }
        }

        protected void clearEntities(bool preserveStatics = true) {
            if (preserveStatics) {
                IList<IEntity> staticEntities = new List<IEntity>(_entities.Where((x) => x.layer == ENTITY_LAYER_STATIC));
                _entities.Clear();
                foreach (IEntity ent in staticEntities) {
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
