using System;
using SFML.Audio;

namespace RoguePanda.asset {
    class AudioAsset : AssetBase {
        private WeakReference _buffer;

        public Music music {
            get {
                Music m = new Music(_filePath);
                return m;
            }
        }

        public Sound sound {
            get {
                if (_buffer == null) {
                    SoundBuffer sb = new SoundBuffer(fileStream);
                    _buffer = new WeakReference(sb);
                }
                Sound s = new Sound(_buffer.Target as SoundBuffer);
                return s;
            }
        }

        public AudioAsset(string filePath) : base(filePath) {
            
        }

        protected override AssetType getAssetType() {
            return AssetType.Audio;
        }
    }
}
