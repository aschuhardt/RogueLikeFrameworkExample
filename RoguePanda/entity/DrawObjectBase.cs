using RoguePanda.entity.entitycolor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.entity {
    internal class DrawObjectBase : IDrawObject {
        protected string _content;
        protected DrawObjectColor _foreColor;
        protected DrawObjectColor _backColor;
        protected float _x;
        protected float _y;
        protected float _layer;

        string IDrawObject.contents {
            get {
                return _content;
            }
        }

        DrawObjectColor IDrawObject.foreColor {
            get {
                return _foreColor;
            }
        }

        DrawObjectColor IDrawObject.backColor {
            get {
                return _backColor;
            }
        }

        float IDrawObject.x {
            get {
                return _x;
            }

            set {
                _x = value;
            }
        }

        float IDrawObject.y {
            get {
                return _y;
            }

            set {
                _y = value;
            }
        }

        float IDrawObject.layer {
            get {
                return _layer;
            }

            set {
                _layer = value;
            }
        }

        protected DrawObjectBase() { }
    }
}
