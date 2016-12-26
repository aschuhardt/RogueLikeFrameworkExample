using System;
using System.Collections.Generic;
using RoguePanda.manager.exceptions;
using SFML.Window;
using SFML.Graphics;

namespace RoguePanda.manager {
    /// <summary>
    /// Manages the detection and buffering of user-input.
    /// </summary>
    internal class InputManager : ManagerBase {
        //needs to have a reference to the active window so that we can handle its input events
        public RenderWindow window { get; set; }

        public InputType inputStatus {
            get {
                return _currentInput;
            }
        }

        public string keyPressed {
            get {
                string keyChar = _keyPressed;
                _keyPressed = "";
                return keyChar;
            }
        }

        private InputType _currentInput;
        private string _keyPressed;
        private ICollection<InputType> _inputBuffer;

        public InputManager() {
            window = null;
            _inputBuffer = new List<InputType>();
        }

        public override bool init() {
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
                case Keyboard.Key.Back:
                    _inputBuffer.Add(InputType.BackSpace);
                    break;
                default:
                    break;
            }

            _keyPressed = getKeyPressedString(e.Code);
            if (!e.Shift) _keyPressed = _keyPressed.ToLower();
        }

        private string getKeyPressedString(Keyboard.Key input) {
            string result = "";

            if (((int)input >= 0 && (int)input <= 25) || input == Keyboard.Key.Space) {
                if (input == Keyboard.Key.Space) {
                    result = " ";
                } else {
                    result = input.ToString();
                }
            }
            return result;
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
