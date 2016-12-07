using System;

namespace roguelike.modules.game_core.player {
    class PlayerBase : IPlayer {
        private Guid _id;

        public Guid id {
            get {
                return _id;
            }
        }

        protected PlayerBase() {
            _id = Guid.NewGuid();
        }
    }
}
