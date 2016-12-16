using RoguePanda.entity;
using RoguePanda.manager.exceptions;
using System.Linq;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using System;
namespace RoguePanda.manager {
    /// <summary>
    /// Manages window initialization and runs drawing routines.
    /// </summary>
    internal sealed class DrawManager : ManagerBase, IDisposable {

        private RenderWindow _window;
        private Font _font;
        private bool _entityBufferSet;
        private bool _windowInitialized;
        private ICollection<IDrawObject> _entityBuffer;
        
        public RenderWindow window {
            get {
                if (!_windowInitialized) {
                    throw new WindowNotInitializedException("Attempted to acquire render window handle before it was fully initialized.");
                } else {
                    return _window;
                }
            }
        }

        /// <summary>
        /// The constructor for the DrawManager object.
        /// Initializes the entity buffer, as well as the entityBufferSet and windowInitialized flags.
        /// </summary>
        public DrawManager() {
            _entityBuffer = new List<IDrawObject>();
            _entityBufferSet = false;
            _windowInitialized = false;
        }

        /// <summary>
        /// Initializes the SFML window and loads the default font.
        /// </summary>
        /// <returns>True if successful, false if not.</returns>
        public bool init() {
            try {
                VideoSettings defaultVideoSettings = new VideoSettings() {
                    width = Convert.ToUInt32(ConfigManager.Instance.Configuration.DefaultWindowWidth),
                    height = Convert.ToUInt32(ConfigManager.Instance.Configuration.DefaultWindowHeight),
                    aalevel = Convert.ToUInt32(ConfigManager.Instance.Configuration.AntialiasingLevel),
                    fullscreen = false
                };

                initWindow(defaultVideoSettings);
                initFont();
                return true;
            } catch (System.Exception ex) {
                errorMessage = $"Failed to initialize drawing manager: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Populates the DrawManager's collection of IEntity objects that will be drawn on the window.
        /// </summary>
        /// <param name="entities"></param>
        public void setEntityBuffer(IEnumerable<IDrawObject> entities) {
            foreach (IDrawObject ent in entities) _entityBuffer.Add(ent);
            _entityBufferSet = true;
        }

        /// <summary>
        /// Checks that the Window has been opened and the entity buffer has been set, then performs all of the routines necessary to draw a single frame.
        /// Clears the entity buffer after drawing has completed and resets the _entityBufferSet to false.
        /// </summary>
        public override void run() {
            if (!_window.IsOpen()) return;

            //check entity-set flag
            if (!_entityBufferSet) {
                throw new DrawManagerEntityBufferNotSetException("Entity buffer was not set before DrawManager.run was called.");
            } else {
                //clear screen buffer
                _window.Clear(Color.Black);

                //do draw routines
                IEnumerable<IDrawObject> sortedEntities = _entityBuffer.OrderBy((x) => x.layer);
                foreach (IDrawObject ent in sortedEntities) {
                    //init colors
                    Color backColor = new Color(ent.backColor.R, ent.backColor.G, ent.backColor.B);
                    Color foreColor = new Color(ent.foreColor.R, ent.foreColor.G, ent.foreColor.B);

                    //init text object
                    Text txt = new Text(ent.contents, _font, Convert.ToUInt32(ConfigManager.Instance.Configuration.FontHeight));
                    txt.Position = new Vector2f(ent.x, ent.y);
                    txt.Color = foreColor;

                    //init background rectangle
                    FloatRect backRect = txt.GetLocalBounds();
                    RectangleShape backRectFill = new RectangleShape(new Vector2f(backRect.Width, backRect.Height + ConfigManager.Instance.Configuration.FontBackgroundHeightMod));
                    backRectFill.FillColor = backColor;
                    backRectFill.Position = new Vector2f(txt.Position.X, txt.Position.Y + ConfigManager.Instance.Configuration.FontBackgroundVerticalPositionMod);

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
        
        /// <summary>
        /// Initializes the window with a given set of video settings.
        /// </summary>
        /// <param name="settings">The video settings to use for confuring the window.</param>
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
                ConfigManager.Instance.Configuration.WindowTitle,
                windowStyle,
                new ContextSettings(Convert.ToUInt32(ConfigManager.Instance.Configuration.BitDepth), Convert.ToUInt32(ConfigManager.Instance.Configuration.StencilDepth), settings.aalevel));
            _window.SetVerticalSyncEnabled(true);
            _windowInitialized = true;
            _window.SetActive(true);
        }

        /// <summary>
        /// Loads the font.
        /// </summary>
        private void initFont() {
            _font = new Font(ConfigManager.Instance.Configuration.FontPath);
        }

        public void Dispose() {
            _window.Dispose();
            _font.Dispose();
        }
    }
}
