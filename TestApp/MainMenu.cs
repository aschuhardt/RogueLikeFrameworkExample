using System.Collections.Generic;
using RoguePanda.drawobject.color;
using RoguePanda.modules;
using RoguePanda.manager;
using System;

namespace testmodule {
    class MainMenu : ModuleBase {
        private float dx = 1.0f;
        private float dy = 1.0f;
        private float x = 0.0f;
        private float y = 0.0f;
        private string text = "asdf";

        private int _lastTicks;
        private int numFrames;

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
            numFrames = 0;
            _lastTicks = Environment.TickCount;
            labels = new List<label>();
            return true;
        }

        private float frameRate() {
            int curTicks = Environment.TickCount;
            if (curTicks - _lastTicks < 1000) {
                numFrames++;
                return -1.0f;
            } else {
                _lastTicks = curTicks;
                int x = numFrames;
                numFrames = 0;
                return x;
            }
        }

        protected override void runModule() {
            clearDrawObjects();
            
            foreach (label l in labels) {
                addDrawObject(l.id.ToString(), DrawObjectColor.createRGB(255, 240, 255), DrawObjectColor.createRGB(0, 0, 0), l.x, l.y);
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
                addDrawObject(string.Format("FPS: {0}", fps.ToString("0.###")), DrawObjectColor.createRGB(255, 240, 255), DrawObjectColor.createRGB(0, 0, 0), 0, 0, true);
            }
        }
    }
}
