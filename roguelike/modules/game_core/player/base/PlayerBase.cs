using System;

namespace roguelike.modules.game_core.player {
    class PlayerBase : IPlayer {
        private Guid _id;

        public Guid id {
            get {
                return _id;
            }
        }

        public int x {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public int y {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public float hitpoints {
            get {
                throw new NotImplementedException();
            }
        }

        protected PlayerBase() {
            _id = Guid.NewGuid();
        }

        public bool takeDamage(float amount, DamageType dmgType) {
            throw new NotImplementedException();
        }
    }
}
