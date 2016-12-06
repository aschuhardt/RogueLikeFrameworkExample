using System.Collections.Generic;
using System.Linq;
using roguelike.entity;
using roguelike.entity.entitycolor;

namespace roguelike.modules {
    class NameEntry : ModuleBase {
        private const float NAME_INPUT_LAYER = -1.0f;
        private const int NAME_MAX_LENGTH = 16;
        private const float PROMPT_Y = 100;
        private const string PROMPT_CHARACTER = "█";
        private const float NAME_Y = 300;
        private string _playerName;

        protected override State getModuleState() {
            return State.NameEntry;
        }

        protected override bool initModule(IList<object> parameters) {
            _playerName = "";
            drawPlayerName();
            drawPrompt();
            return true;
        }

        protected override void runModule() {
            bool redraw = true;
            if (keyPressed != "" && !testInput(InputType.BackSpace) && _playerName.Length <= NAME_MAX_LENGTH) {
                _playerName += keyPressed;
            } else if (testInput(InputType.BackSpace) && _playerName.Length > 0) {
                _playerName = _playerName.Remove(_playerName.Length - 1, 1);
            } else if (testInput(InputType.Enter)) {
                IList<object> paramList = new List<object>();
                paramList.Add(_playerName);
                transitionToState(State.Play, paramList);
                redraw = false;
            } else {
                redraw = false;
            }

            if (redraw) drawPlayerName();
        }

        private void drawPlayerName() {
            //clear old name text but avoid recreating everything else
            IEnumerable<IEntity> staticText = new List<IEntity>(_entities.Where((x) => x.layer != NAME_INPUT_LAYER));
            _entities.Clear();
            foreach (IEntity ent in staticText) {
                _entities.Add(ent);
            }

            EntityColor nameForeColor = EntityColor.createRGB(192, 158, 113);
            EntityColor nameBackColor = EntityColor.createRGB(0, 0, 0);
            string nameDisplay = _playerName + PROMPT_CHARACTER;
            float left = _windowWidth / 2 - ((nameDisplay.Length * GlobalStatics.FONT_WIDTH) / 2);
            _entities.Add(new FlexibleEntity(nameDisplay, nameForeColor, nameBackColor, left, NAME_Y, NAME_INPUT_LAYER));
        }

        private void drawPrompt() {
            EntityColor promptForeColor = EntityColor.createRGB(76, 103, 123);
            EntityColor promptBackColor = EntityColor.createRGB(0, 0, 0);
            string promptText = "Type your name, then press ENTER:";
            float promptLeft = _windowWidth / 2 - ((promptText.Length * GlobalStatics.FONT_WIDTH) / 2);
            _entities.Add(new FlexibleEntity(promptText, promptForeColor, promptBackColor, promptLeft, PROMPT_Y));
        }
    }
}
