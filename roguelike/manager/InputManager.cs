﻿using System;
using System.Collections.Generic;
using roguelike.manager.exceptions;
using SFML.Window;
using SFML.Graphics;

namespace roguelike.manager {
    class InputManager : ManagerBase {
        public RenderWindow window { get; set; }
        public InputType inputStatus {
            get {
                return _currentInput;
            }
        }
        
        private InputType _currentInput;
        private ICollection<InputType> _inputBuffer;

        public InputManager() {
            window = null;
            _inputBuffer = new List<InputType>();
        }

        public bool init() {
            try {
                if (window == null) {
                    throw new InputManagerWindowHandleNotSetException("Attempted to perform InputManager.run before window handle was set.");
                } else {
                    //set handlers for window events
                    window.KeyPressed += HandleKeyPressed;
                    window.Closed += HandleWindowClosed;
                    window.Resized += HandleWindowResized;
                    return true;
                }
            } catch (Exception ex) {
                errorMessage = $"Failed to initialize input manager: {ex.Message}";
                return false;
            }
        }

        private void HandleWindowResized(object sender, SizeEventArgs e) {
            window.Size = new Vector2u(e.Width, e.Height);
        }

        private void HandleWindowClosed(object sender, EventArgs e) {
            _inputBuffer.Add(InputType.Quit);
        }

        private void HandleKeyPressed(object sender, KeyEventArgs e) {
            switch (e.Code) {
                case Keyboard.Key.Left:
                    _inputBuffer.Add(InputType.Left);
                    break;
                case Keyboard.Key.Right:
                    _inputBuffer.Add(InputType.Right);
                    break;
                case Keyboard.Key.Up:
                    _inputBuffer.Add(InputType.Up);
                    break;
                case Keyboard.Key.Down:
                    _inputBuffer.Add(InputType.Down);
                    break;
                case Keyboard.Key.Return:
                    _inputBuffer.Add(InputType.Enter);
                    break;
                case Keyboard.Key.Escape:
                    _inputBuffer.Add(InputType.Escape);
                    break;
                default:
                    break;
            }
        }

        public override void run() {
            window.DispatchEvents();
            _currentInput = InputType.None;
            foreach (InputType input in _inputBuffer) {
                InputFlagHelper.setFlag(ref _currentInput, input);
            }
            _inputBuffer.Clear();
        }
    }
}
