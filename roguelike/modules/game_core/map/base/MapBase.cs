using System;

namespace roguelike.modules.game_core.map {
    class MapBase : IMap {

        private Guid _id;

        public Guid id {
            get {
                return _id;
            }
        }

        protected MapBase() {
            _id = Guid.NewGuid();
        }
    }
}
