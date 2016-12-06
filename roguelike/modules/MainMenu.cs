using System.Linq;
using System.Collections.Generic;
using roguelike.entity;
using roguelike.entity.entitycolor;

namespace roguelike.modules {
    class MainMenu : ModuleBase {
        public enum MenuOption {
            Play,
            Options,
            About
        }

        private const string MENU_PREFIX_SELECTED = "> ";
        private const string MENU_PREFIX_NOTSELECTED = "  ";
        private const float TITLE_X = 130.0f;
        private const float TITLE_Y = 20.0f;
        private const float MENU_X = 64.0f;
        private const float MENU_Y = 250.0f;
        private const float MENU_ITEM_SPACING = 16.0f;
        private const float MENU_OPTIONS_LAYER = 1.0f;
        private const float STATIC_LAYER = -1.0f;

        private MenuOption _selectedOption;
        private EntityColor _borderForeColor;
        private EntityColor _borderBackColor;
        private EntityColor _menuItemForeColor;
        private EntityColor _menuItemBackColor;
        private EntityColor _titleForeColor;
        private EntityColor _titleBackColor;
        private bool _scrollingUp;
        private bool _scrollingDown;
        private bool _redrawMenu;

        protected override State getModuleState() {
            return State.MainMenu;
        }

        protected override bool initModule(IList<object> parameters) {
            //main menu state can't receive parameters
            _borderForeColor = EntityColor.createRGB(10, 33, 23);
            _borderBackColor = EntityColor.createRGB(0, 0, 0);
            _menuItemForeColor = EntityColor.createRGB(76, 103, 123);
            _menuItemBackColor = EntityColor.createRGB(0, 0, 0);
            _titleForeColor = EntityColor.createRGB(192, 158, 113);
            _titleBackColor = EntityColor.createRGB(10, 33, 23);
            _scrollingUp = false;
            _scrollingDown = false;
            _redrawMenu = true;

            drawStaticGlyphs();

            _selectedOption = MenuOption.Play;
            return true;
        }

        protected override void runModule() {
            processInput();

            if (_redrawMenu) {
                //clear item layer from entities
                IEnumerable<IEntity> nonMenuItems = new List<IEntity>(_entities.Where((x) => x.layer != MENU_OPTIONS_LAYER));
                _entities.Clear();
                foreach (IEntity ent in nonMenuItems) {
                    _entities.Add(ent);
                }
                
                //draw menu items
                float menuLineOffset = 0.0f;
                foreach (MenuOption opt in System.Enum.GetValues(typeof(MenuOption))) {
                    string menuLineText = "";
                    if (_selectedOption == opt) {
                        menuLineText += MENU_PREFIX_SELECTED;
                    } else {
                        menuLineText += MENU_PREFIX_NOTSELECTED;
                    }
                    menuLineText += opt.ToString();
                    _entities.Add(new FlexibleEntity(menuLineText, _menuItemForeColor, _menuItemBackColor, MENU_X, MENU_Y + menuLineOffset, MENU_OPTIONS_LAYER));
                    menuLineOffset += MENU_ITEM_SPACING;

                    _redrawMenu = false;
                }

            }
        }

        private void drawStaticGlyphs() {
            //draw borders
            foreach (IEntity ent in EntityDrawingMacros.drawWindowBorders(_windowWidth, _windowHeight, _borderForeColor, _borderBackColor, STATIC_LAYER)) {
                _entities.Add(ent);
            }

            //draw title
            string[] titleLines = EntityDrawingMacros.splitMultiLineDelimited(GlobalStatics.TITLE_ART);
            int lineIndex = 0;
            foreach (string line in titleLines) {
                IEntity lineEnt = new FlexibleEntity(line, _titleForeColor, _titleBackColor, TITLE_X, TITLE_Y + lineIndex, STATIC_LAYER);
                _entities.Add(lineEnt);
                lineIndex += GlobalStatics.FONT_HEIGHT;
            }
        }

        private void processInput() {
            MenuOption[] options = (MenuOption[])System.Enum.GetValues(typeof(MenuOption));
            int selIndex = (int)_selectedOption;
            bool downPressed = InputFlagHelper.isInputFlagSet(_input, InputType.Down);
            bool upPressed = InputFlagHelper.isInputFlagSet(_input, InputType.Up);
            bool enterPressed = InputFlagHelper.isInputFlagSet(_input, InputType.Enter);

            if (!_scrollingDown && downPressed && selIndex < options.Length - 1) {
                _selectedOption = options[selIndex + 1];
                _scrollingDown = true;
                _redrawMenu = true;
            } else if (_scrollingDown && !downPressed) {
                _scrollingDown = !_scrollingDown;
            }

            if (!_scrollingUp && upPressed && selIndex > 0) {
                _selectedOption = options[selIndex - 1];
                _scrollingUp = true;
                _redrawMenu = true;
            } else if (_scrollingUp && !upPressed) {
                _scrollingUp = !_scrollingUp;
            }
            
            if (enterPressed) {                
                switch (_selectedOption) {
                    case MenuOption.Play:
                        transitionToState(State.Play);
                        break;
                    case MenuOption.Options:
                        transitionToState(State.Options);
                        break;
                    case MenuOption.About:
                        transitionToState(State.About);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
