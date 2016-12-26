using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.entity {
    class SimpleAudioEntity : AudioEntityBase {
        public SimpleAudioEntity(string assetName, AudioEntityType audioType, string tag = "", float volume = 100, float pitch = 1, bool loop = false) : base(audioType, tag) {
            this.assetID = assetName;
            this.volume = volume;
            this.pitch = pitch;
            this.loop = loop;
        }
    }
}
