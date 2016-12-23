using RoguePanda.entity.color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.entity {
    internal class TextObjectBase : ITextObject {
        protected string _content;
        protected EntityColor _foreColor;
        protected EntityColor _backColor;
        protected float _x;
        protected float _y;
        protected float _layer;

        string ITextObject.contents {
            get {
                return _content;
            }
        }

        EntityColor ITextObject.foreColor {
            get {
                return _foreColor;
            }
        }

        EntityColor ITextObject.backColor {
            get {
                return _backColor;
            }
        }

        float ITextObject.x {
            get {
                return _x;
            }

            set {
                _x = value;
            }
        }

        float ITextObject.y {
            get {
                return _y;
            }

            set {
                _y = value;
            }
        }

        float ITextObject.layer {
            get {
                return _layer;
            }

            set {
                _layer = value;
            }
        }

        protected TextObjectBase() { }
    }
}
