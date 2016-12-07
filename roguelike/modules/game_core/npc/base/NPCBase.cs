using System;

namespace roguelike.modules.game_core.npc {
    class NPCBase : INPC {

        private Guid _id;

        public Guid id {
            get {
                return _id;
            }
        }

        protected NPCBase() {
            _id = Guid.NewGuid();
        }
    }
}
