using System.Collections.Generic;
using roguelike.entity;

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

        private MenuOption _selectedOption;
        private bool _scrollingUp;
        private bool _scrollingDown;
        private bool _redrawMenu;

        protected override State getModuleState() {
            return State.MainMenu;
        }

        protected override bool initModule(IList<object> parameters) {
            //main menu state can't receive parameters
            _scrollingUp = false;
            _scrollingDown = false;
            _redrawMenu = true;

            drawStaticEntities();

            _selectedOption = MenuOption.Play;
            return true;
        }

        protected override void runModule() {
            processInput();

            if (_redrawMenu) {
                //clear item layer from entities
                clearEntities();

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
                    addEntity(menuLineText, Colors.DarkText_ForeColor, Colors.DarkText_BackColor, MENU_X, MENU_Y + menuLineOffset);
                    menuLineOffset += MENU_ITEM_SPACING;

                    _redrawMenu = false;
                }

            }
        }

        private void drawStaticEntities() {
            //draw borders
            drawBorders();

            //draw title
            string[] titleLines = EntityDrawingMacros.splitMultiLineDelimited(GlobalStatics.TITLE_ART);
            int lineIndex = 0;
            foreach (string line in titleLines) {
                float titleLeft = _windowWidth / 2 - ((line.Length * GlobalStatics.FONT_WIDTH) / 2);
                addEntity(line, Colors.Title_ForeColor, Colors.Title_BackColor, titleLeft, TITLE_Y + lineIndex, true);
                lineIndex += GlobalStatics.FONT_HEIGHT;
            }
        }

        private void processInput() {
            MenuOption[] options = (MenuOption[])System.Enum.GetValues(typeof(MenuOption));
            int selIndex = (int)_selectedOption;
            bool downPressed = testInput(InputType.Down);
            bool upPressed = testInput(InputType.Up);
            bool enterPressed = testInput(InputType.Enter);

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
