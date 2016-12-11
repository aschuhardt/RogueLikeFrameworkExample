﻿using System;
using System.Collections.Generic;

namespace roguelike.modules {
    class Play : ModuleBase {
        private string _playerName = "";

        protected override State getModuleState() {
            return State.Play;
        }

        protected override bool initModule(IList<object> parameters) {
            if (parameters == null) {
                //we didn't get player name, so transition to name entry module
                transitionToState(State.NameEntry);
            } else {
                //get player name from transition params
                _playerName = parameters[0].ToString();
            }

            addEntity($"Hello {_playerName}!", Colors.LightText_ForeColor, Colors.LightText_BackColor, 100, 100, true);
            return true;
        }

        protected override void runModule() {
           
        }
    }
}
