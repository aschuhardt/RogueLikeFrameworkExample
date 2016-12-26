using System;

namespace RoguePanda.entity {
    internal abstract class AudioEntityBase : IAudioEntity {
        public string assetID { get; set; }
        public float volume { get; set; }
        public float pitch { get; set; }
        public bool loop { get; set; }
        public string tag { get; set; }

        private AudioEntityType _audioType;
        public AudioEntityType audioType {
            get {
                return _audioType;
            }
        }

        protected AudioEntityBase(AudioEntityType entAudioType, string tag = "") {
            if (string.IsNullOrEmpty(tag)) {
                //if no tag is provided, assign the object a guid string as its tag
                //doing this because the audio manager will ultimately store audio-related in a dictionary, and the tag will act as key.
                //providing a tag is a convenience on the part of the developer (i.e. can pause/stop individual tracks), but is not mandatory.
                this.tag = Guid.NewGuid().ToString();
            } else {
                this.tag = tag;
            }
            _audioType = entAudioType;
        }
    }
}
