using roguelike.entity;
using roguelike.manager.exceptions;
using System.Linq;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using System;
namespace roguelike.manager {
    class DrawManager : ManagerBase {

        private RenderWindow _window;
        private Font _font;
        private bool _entityBufferSet;
        private bool _windowInitialized;
        private ICollection<IEntity> _entityBuffer;

        public VideoSettings defaultVideoSettings { get; set; }

        public RenderWindow window {
            get {
                if (!_windowInitialized) {
                    throw new WindowNotInitializedException("Attempted to acquire render window handle before it was fully initialized.");
                } else {
                    return _window;
                }
            }
        }

        public DrawManager() {
            _entityBuffer = new List<IEntity>();
            _entityBufferSet = false;
            _windowInitialized = false;
        }

        public bool init() {
            try {
                initWindow();
                initFont();
                return true;
            } catch (System.Exception ex) {
                errorMessage = $"Failed to initialize drawing manager: {ex.Message}";
                return false;
            }
        }

        public void setEntityBuffer(IEnumerable<IEntity> entities) {
            foreach (IEntity ent in entities) _entityBuffer.Add(ent);
            _entityBufferSet = true;
        }

        public override void run() {
            if (!_window.IsOpen()) return;

            //check entity-set flag
            if (!_entityBufferSet) {
                throw new DrawManagerEntityBufferNotSetException("Entity buffer was not set before DrawManager.run was called.");
            } else {
                //clear screen buffer
                _window.Clear(Color.Black);

                //do draw routines
                IEnumerable<IEntity> sortedEntities = _entityBuffer.OrderBy((x) => x.layer);
                foreach (IEntity ent in sortedEntities) {
                    //init colors
                    Color backColor = new Color(ent.backColor.R, ent.backColor.G, ent.backColor.B);
                    Color foreColor = new Color(ent.foreColor.R, ent.foreColor.G, ent.foreColor.B);

                    //init text object
                    Text txt = new Text(ent.contents, _font, GlobalStatics.FONT_HEIGHT);
                    txt.Position = new Vector2f(ent.x, ent.y);
                    txt.Color = foreColor;

                    //init background rectangle
                    FloatRect backRect = txt.GetLocalBounds();
                    RectangleShape backRectFill = new RectangleShape(new Vector2f(backRect.Width, backRect.Height + GlobalStatics.FONT_BACKGROUND_HEIGHT_ADJUSTMENT));
                    backRectFill.FillColor = backColor;
                    backRectFill.Position = new Vector2f(txt.Position.X, txt.Position.Y + GlobalStatics.FONT_BACKGROUND_VERT_POS_ADJUSTMENT);

                    //draw background rectangle
                    _window.Draw(backRectFill);

                    //draw text
                    _window.Draw(txt);

                }

                //show drawn screen buffer
                _window.Display();

                //clear entity buffer and reset flag
                _entityBuffer.Clear();
                _entityBufferSet = false;
            }
        }

        public void initWindow() {
            initWindow(defaultVideoSettings);
        }

        public void initWindow(VideoSettings settings) {
            Styles windowStyle = Styles.Close | Styles.Titlebar;
            if (settings.fullscreen) {
                windowStyle |= Styles.Fullscreen;
            }

            if (_windowInitialized) {
                _window.Dispose();
            }

            _window = new RenderWindow(
                new VideoMode(settings.width, settings.height),
                GlobalStatics.WINDOW_TITLE,
                windowStyle,
                new ContextSettings(GlobalStatics.BIT_DEPTH, GlobalStatics.STENCIL_DEPTH, settings.aalevel));
            _window.SetVerticalSyncEnabled(true);
            _windowInitialized = true;
            _window.SetActive(true);
        }

        private void initFont() {
            _font = new Font(GlobalStatics.FONT_PATH);
        }
    }
}
