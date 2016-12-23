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
        public VideoSettings videoSettings { get; private set; }
        private RenderWindow _window;
        private Font _font;
        private bool _textEntityBufferSet;
        private bool _spriteEntityBufferSet;
        private bool _windowInitialized;
        private ICollection<ITextObject> _textEntityBuffer;
        private ICollection<ISpriteObject> _spriteEntityBuffer;

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
            _textEntityBuffer = new List<ITextObject>();
            _spriteEntityBuffer = new List<ISpriteObject>();
            _textEntityBufferSet = false;
            _spriteEntityBufferSet = false;
            _windowInitialized = false;
        }

        /// <summary>
        /// Initializes the SFML window and loads the default font.
        /// </summary>
        /// <returns>True if successful, false if not.</returns>
        public bool init() {
            try {
                VideoSettings defaultVideoSettings = new VideoSettings() {
                    width = Convert.ToUInt32(ConfigManager.Config.DefaultWindowWidth),
                    height = Convert.ToUInt32(ConfigManager.Config.DefaultWindowHeight),
                    aalevel = Convert.ToUInt32(ConfigManager.Config.AntialiasingLevel),
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
        public void setTextEntityBuffer(IEnumerable<ITextObject> entities) {
            foreach (ITextObject ent in entities) _textEntityBuffer.Add(ent);
            _textEntityBufferSet = true;
        }

        public void setSpriteEntityBuffer(IEnumerable<ISpriteObject> entities) {
            foreach (ISpriteObject ent in entities) _spriteEntityBuffer.Add(ent);
            _spriteEntityBufferSet = true;
        }

        /// <summary>
        /// Checks that the Window has been opened and the entity buffer has been set, then performs all of the routines necessary to draw a single frame.
        /// Clears the entity buffer after drawing has completed and resets the _entityBufferSet to false.
        /// </summary>
        public override void run() {
            if (!_window.IsOpen()) return;

            //clear screen buffer
            _window.Clear(Color.Black);

            //check entity-set flag
            if (!_textEntityBufferSet) {
                throw new DrawManagerEntityBufferNotSetException("Entity buffer was not set before DrawManager.run was called.");
            } else {

                //do TEXT draw routines
                IEnumerable<ITextObject> sortedTextEntities = _textEntityBuffer.OrderBy((x) => x.layer);
                foreach (ITextObject ent in sortedTextEntities) {
                    //init colors
                    Color backColor = new Color(ent.backColor.R, ent.backColor.G, ent.backColor.B);
                    Color foreColor = new Color(ent.foreColor.R, ent.foreColor.G, ent.foreColor.B);

                    //init text object
                    Text txt = new Text(ent.contents, _font, Convert.ToUInt32(ConfigManager.Config.FontHeight));
                    txt.Position = new Vector2f(ent.x, ent.y);
                    txt.Color = foreColor;

                    //init background rectangle
                    FloatRect backRect = txt.GetLocalBounds();
                    RectangleShape backRectFill = new RectangleShape(new Vector2f(backRect.Width, backRect.Height + ConfigManager.Config.FontBackgroundHeightMod));
                    backRectFill.FillColor = backColor;
                    backRectFill.Position = new Vector2f(txt.Position.X, txt.Position.Y + ConfigManager.Config.FontBackgroundVerticalPositionMod);

                    //draw background rectangle
                    _window.Draw(backRectFill);

                    //draw text
                    _window.Draw(txt);
                }

                //clear entity buffer and reset flag
                _textEntityBuffer.Clear();
                _textEntityBufferSet = false;
            }

            //do SPRITE drawing routines
            if (!_spriteEntityBufferSet) {
                throw new DrawManagerEntityBufferNotSetException("Entity buffer was not set before DrawManager.run was called.");
            } else {
                IEnumerable<ISpriteObject> sortedSpriteEntities = _spriteEntityBuffer.OrderBy((x) => x.layer);
                foreach (ISpriteObject ent in sortedSpriteEntities) {
                    IntRect spriteTextureRect = new IntRect(ent.texPosX, ent.texPosY, ent.width, ent.height);
                    Sprite spr = new Sprite(TextureMapper.getTexture(ent.texID), spriteTextureRect);
                    spr.Position = new Vector2f(ent.x, ent.y);
                    spr.Scale = new Vector2f(ent.scaleX, ent.scaleY);
                    //set origin at center of sprite
                    spr.Origin = new Vector2f(ent.x + (ent.width / 2), ent.y + (ent.height / 2));
                    spr.Rotation = ent.rotation;
                    spr.Color = new Color(ent.color.R, ent.color.G, ent.color.B, ent.alpha);
                    window.Draw(spr);
                }

                //clear entity buffer and reset flag
                _spriteEntityBuffer.Clear();
                _spriteEntityBufferSet = false;
            }

            //show drawn screen buffer
            _window.Display();
        }

        /// <summary>
        /// Initializes the window with a given set of video settings.
        /// </summary>
        /// <param name="settings">The video settings to use for configuring the window.</param>
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
                ConfigManager.Config.WindowTitle,
                windowStyle,
                new ContextSettings(Convert.ToUInt32(ConfigManager.Config.BitDepth), Convert.ToUInt32(ConfigManager.Config.StencilDepth), settings.aalevel));
            _window.SetVerticalSyncEnabled(true);
            _windowInitialized = true;
            _window.SetActive(true);
            videoSettings = settings;
        }

        /// <summary>
        /// Loads the font.
        /// </summary>
        private void initFont() {
            _font = new Font(ConfigManager.Config.FontPath);
        }

        public void Dispose() {
            _window.Dispose();
            _font.Dispose();
        }
    }
}
