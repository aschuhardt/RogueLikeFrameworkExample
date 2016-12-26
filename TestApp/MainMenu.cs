using System.Collections.Generic;
using RoguePanda.entity.color;
using RoguePanda.modules;
using RoguePanda.manager;
using System;

namespace testmodule {
    class MainMenu : ModuleBase {
        private int _lastTicks;
        private int _curFrameCounter;

        private List<label> labels;

        private class label {
            public int id { get; set; }
            public float x { get; set; }
            public float y { get; set; }
        }

        protected override string getModuleState() {
            return "testmodule.MainMenu";
        }

        protected override bool initModule(IList<object> parameters) {
            _curFrameCounter = 0;
            _lastTicks = Environment.TickCount;
            labels = new List<label>();
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
            
            foreach (label l in labels) {
                addTextObject(l.id.ToString(), EntityColor.createRGB(255, 240, 255), EntityColor.createRGB(0, 0, 0), l.x, l.y);
            }

            if (testInput(RoguePanda.InputType.Enter)) {
                Random rand = new Random(Environment.TickCount);
                label l = new label();
                l.id = labels.Count;
                l.x = rand.Next(0, Convert.ToInt32(_windowWidth - ConfigManager.Config.FontWidth));
                l.y = rand.Next(0, Convert.ToInt32(_windowHeight - ConfigManager.Config.FontHeight));
                labels.Add(l);
            }

            float fps = frameRate();
            if (fps != -1.0f) {
                addTextObject(string.Format("FPS: {0}", fps.ToString("0.###")), EntityColor.createRGB(255, 240, 255), EntityColor.createRGB(0, 0, 0), 0, 0, true);
            }
        }
    }
}
