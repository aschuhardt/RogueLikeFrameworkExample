using System.Collections.Generic;
using RoguePanda.entity.color;
using RoguePanda.modules;
using RoguePanda.manager;
using System;

namespace testmodule {
    class MainMenu : ModuleBase {
        private int _lastTicks;
        private int _curFrameCounter;

        private float _rotationAmount;

        private List<label> labels;

        private class label {
            public int id { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

        protected override string getModuleState() {
            return "testmodule.MainMenu";
        }

        protected override bool initModule(IList<object> parameters) {
            _curFrameCounter = 0;
            _lastTicks = Environment.TickCount;
            labels = new List<label>();
            _rotationAmount = 0.0f;
            return true;
        }

        private float frameRate() {
            int curTicks = Environment.TickCount;
            if (curTicks - _lastTicks < 1000) {
                _curFrameCounter++;
                return -1.0f;
            } else {
                _lastTicks = curTicks;
                int frames = _curFrameCounter;
                _curFrameCounter = 0;
                return frames;
            }
        }

        protected override void runModule() {
            clearDrawObjects();

            //addSimpleSpriteObject("dummy", 128, 128, false, x: 400, y: 400, rotation: _rotationAmount, scaleX: 0.2f, scaleY: 0.2f);

            _rotationAmount += 1.0f;
            _rotationAmount = _rotationAmount % 360;

            foreach (label l in labels) {
                //addTextObject(l.id.ToString(), EntityColor.createRGB(255, 240, 255), EntityColor.createRGB(0, 0, 0), l.x, l.y);
                addSimpleSpriteObject("dummy", 128, 128, false, x: l.y, y: l.x, rotation: _rotationAmount, scaleX: 0.4f, scaleY: 0.4f);
            }

            if (testInput(RoguePanda.InputType.Enter)) {
                Random rand = new Random(Environment.TickCount);
                label l = new label();
                l.id = labels.Count;
                l.x = rand.Next(0, Convert.ToInt32(_windowWidth - ConfigManager.Config.FontWidth));
                l.y = rand.Next(0, Convert.ToInt32(_windowHeight - ConfigManager.Config.FontHeight));
                labels.Add(l);
                addTextObject(string.Format("Enter is pressed!"), EntityColor.createRGB(255, 240, 255), EntityColor.createRGB(0, 0, 0), 200, 0, false);
            } else {
                addTextObject(string.Format("Enter is NOT pressed!"), EntityColor.createRGB(255, 240, 255), EntityColor.createRGB(0, 0, 0), 200, 0, false);
            }

            float fps = frameRate();
            if (fps != -1.0f) {
                addTextObject(string.Format("FPS: {0}", fps.ToString("0.###")), EntityColor.createRGB(255, 240, 255), EntityColor.createRGB(0, 0, 0), 0, 0, true);
            }
        }
    }
}
