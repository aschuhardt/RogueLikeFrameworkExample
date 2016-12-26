using System;
using System.Reflection;
using System.IO;
using RoguePanda.manager;

namespace RoguePanda {

    /// <summary>
    /// The top-level engine of this game framework.
    /// Manages the behavior of and interactions between Logic, Input, and Drawing managers.
    /// </summary>
    public class Game : IDisposable {
        public string errorMessage { get; }
        private bool _shouldQuit = false;
        private bool _initSuccess = false;
        private DrawManager _drawMan;
        private LogicManager _logicMan;
        private InputManager _inputMan;
        private AudioManager _audioMan;

        /// <summary>
        /// The Game object's constructor.
        /// Initializes manager objects, and sets video settings to their default values.
        /// If any manager fails to initialize, its error message will be appended onto the "errorMessage" string that is a member of this object here.
        /// </summary>
        public Game() {
            //extract required libs if they don't exist
            extractDependencies();

            _drawMan = new DrawManager();
            _logicMan = new LogicManager();
            _inputMan = new InputManager();
            _audioMan = new AudioManager();
            

            if (init()) {
                _initSuccess = true;
            } else {
                //get error messages from managers
                errorMessage = string.Join("; ", new string[] {
                    _drawMan.errorMessage,
                    _logicMan.errorMessage,
                    _drawMan.errorMessage,
                    _audioMan.errorMessage
                });
            }

        }

        /// <summary>
        /// Begins and maintains the main input/logic/drawing loop for the Game.
        /// If any managers failed to initialize in the Game object's constructor, then their error messages will be printed to the console at the start of this method.
        /// </summary>
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
                            //populate draw manager's entity buffers
                            _drawMan.setTextEntityBuffer(_logicMan.textEntities);
                            _drawMan.setSpriteEntityBuffer(_logicMan.spriteEntities);

                            //perform draw routines
                            _drawMan.run();
                        }

                        //run audio routines
                        _audioMan.addAudioEntities(_logicMan.audioEntities);
                        _audioMan.addStopTags(_logicMan.requestedAudioStopTags);
                        _audioMan.run();
                        
                    }
                }
            }
        }

        /// <summary>
        /// Executes initialization procedures.
        /// </summary>
        /// <returns></returns>
        private bool init() {
            bool result = true;
            
            result &= _drawMan.init();

            //set inputMan's window handle
            _inputMan.window = _drawMan.window;

            _logicMan.videoSettings = _drawMan.videoSettings;
            result &= _logicMan.init();

            result &= _inputMan.init();

            result &= _audioMan.init();

            return result;
        }

        public void Dispose() {
            ((IDisposable)_drawMan).Dispose();
        }

        /// <summary>
        /// extract embedded dependencies if they aren't present at startup
        /// </summary>
        private void extractDependencies() {
            Assembly a = Assembly.GetExecutingAssembly();
            string[] depnames = a.GetManifestResourceNames();
            foreach (string dep in depnames) {
                string assemblyName = a.FullName.Split(' ')[0].TrimEnd(',');
                if (dep.StartsWith(assemblyName + ".lib.")) {
                    string fn = Path.GetFileName(dep).Replace(assemblyName + ".lib.", "");
                    if (!File.Exists(fn)) {
                        Stream embedded = a.GetManifestResourceStream(dep);
                        FileStream output = new FileStream(fn, FileMode.Create);
                        for (int i = 0; i < embedded.Length; i++) {
                            output.WriteByte((byte)embedded.ReadByte());
                        }
                        output.Close();
                    }
                }
            }
        }

    }
}
