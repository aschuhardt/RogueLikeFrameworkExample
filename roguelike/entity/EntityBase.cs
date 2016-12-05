using roguelike.entity.entitycolor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike.entity {
    class EntityBase : IEntity {
        protected string _content;
        protected EntityColor _foreColor;
        protected EntityColor _backColor;
        protected float _x;
        protected float _y;
        protected float _layer;

        string IEntity.contents {
            get {
                return _content;
            }
        }

        EntityColor IEntity.foreColor {
            get {
                return _foreColor;
            }
        }

        EntityColor IEntity.backColor {
            get {
                return _backColor;
            }
        }

        float IEntity.x {
            get {
                return _x;
            }

            set {
                _x = value;
            }
        }

        float IEntity.y {
            get {
                return _y;
            }

            set {
                _y = value;
            }
        }

        float IEntity.layer {
            get {
                return _layer;
            }

            set {
                _layer = value;
            }
        }

        protected EntityBase() { }
    }
}
