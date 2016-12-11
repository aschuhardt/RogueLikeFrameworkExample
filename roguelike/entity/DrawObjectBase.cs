using RoguePanda.entity.entitycolor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.entity {
    class DrawObjectBase : IDrawObject {
        protected string _content;
        protected EntityColor _foreColor;
        protected EntityColor _backColor;
        protected float _x;
        protected float _y;
        protected float _layer;

        string IDrawObject.contents {
            get {
                return _content;
            }
        }

        EntityColor IDrawObject.foreColor {
            get {
                return _foreColor;
            }
        }

        EntityColor IDrawObject.backColor {
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
