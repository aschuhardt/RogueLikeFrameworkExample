using System;

namespace roguelike.modules.game_core.item {
    class ItemBase : IItem {
        private Guid _id;

        public Guid id {
            get {
                return _id;
            }
        }

        protected ItemBase() {
            _id = Guid.NewGuid();
        }
    }
}
