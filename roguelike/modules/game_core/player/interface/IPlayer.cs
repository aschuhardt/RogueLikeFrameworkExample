using System;

namespace roguelike.modules.game_core.player {
    interface IPlayer {
        Guid id { get; }
        int x { get; set; }
        int y { get; set; }
        float hitpoints { get; }

        //return false if dodged/avoided
        bool takeDamage(float amount, DamageType dmgType);

    }
}
