﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using roguelike.manager;

namespace roguelike {
    class Game {
        public string errorMessage { get; }

        private bool _shouldQuit = false;
        private bool _initSuccess = false;
        private DrawManager _drawMan;
        private LogicManager _logicMan;
        private InputManager _inputMan;
        private VideoSettings _defaultVideoSettings;

        public Game() {
            _drawMan = new DrawManager();
            _logicMan = new LogicManager();
            _inputMan = new InputManager();

            _defaultVideoSettings = new VideoSettings() {
                width = GlobalStatics.DEFAULT_WINDOW_WIDTH,
                height = GlobalStatics.DEFAULT_WINDOW_HEIGHT,
                aalevel = GlobalStatics.DEFAULT_ANTIALIASING_LEVEL,
                fullscreen = false
            };

            if (init()) {
                _initSuccess = true;
            } else {
                //get error messages from managers
                errorMessage = string.Join("; ", new string[] {
                    _drawMan.errorMessage,
                    _logicMan.errorMessage,
                    _drawMan.errorMessage
                });
            }

        }

        public void run() {
            if (!_initSuccess) {
                //display error information to console
                Console.WriteLine($"Could not run, failed to initialize: {errorMessage}.");
            } else {
                //start main loop
                while (!_shouldQuit) {
                    //process results of user input events
                    _inputMan.run();

                    //check for window closing
                    if (InputFlagHelper.isInputFlagSet(_inputMan.inputStatus, InputType.Quit)) {
                        _shouldQuit = true;
                    } else {
                        //send input to logic manager
                        _logicMan.currentInput = _inputMan.inputStatus;
                        _logicMan.keyPressed = _inputMan.keyPressed;

                        //perform core game logic
                        _logicMan.run();

                        //check whether logicman wants to reinit the window and do so if necessary
                        if (_logicMan.shouldReInitializeWindow) {
                            _drawMan.initWindow(_logicMan.videoSettings);
                            //reinit the input manager since it depends on our window object
                            _inputMan.window = _drawMan.window;
                            _inputMan.init();
                        } else {
                            //populate draw manager's entity buffer
                            _drawMan.setEntityBuffer(_logicMan.getEntities());

                            //perform draw routines
                            _drawMan.run();
                        }
                    }
                }
            }
        }

        private bool init() {
            bool result = true;

            _drawMan.defaultVideoSettings = _defaultVideoSettings;
            result &= _drawMan.init();

            //set inputMan's window handle
            _inputMan.window = _drawMan.window;

            _logicMan.videoSettings = _drawMan.defaultVideoSettings;
            result &= _logicMan.init();

            result &= _inputMan.init();
            return result;
        }
    }
}
