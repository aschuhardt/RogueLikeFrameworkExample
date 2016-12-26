using System.Collections.Generic;
using RoguePanda.entity;
using SFML.Audio;

namespace RoguePanda.manager {
    class AudioManager : ManagerBase {
        private IDictionary<string, Sound> _soundBuffer;
        private IDictionary<string, Music> _musicBuffer;
        private Queue<IAudioEntity> _entityBufferToPlay;
        private Queue<string> _tagsToStop;

        public void addAudioEntities(IEnumerable<IAudioEntity> entities) {
            foreach (IAudioEntity ent in entities) {
                _entityBufferToPlay.Enqueue(ent);
            }
        }

        public void addStopTags(IEnumerable<string> tags) {
            foreach (string tag in tags) {
                _tagsToStop.Enqueue(tag);
            }
        }

        public AudioManager() {
            _soundBuffer = new Dictionary<string, Sound>();
            _musicBuffer = new Dictionary<string, Music>();
            _entityBufferToPlay = new Queue<IAudioEntity>();
            _tagsToStop = new Queue<string>();
        }

        public override bool init() {
            return true;
        }

        public override void run() {
            //start playing requested audio entities
            while (_entityBufferToPlay.Count > 0) {
                IAudioEntity ent = _entityBufferToPlay.Dequeue();
                switch (ent.audioType) {
                    case AudioEntityType.Sound:
                        addSoundToBufferAndPlay(ent);
                        break;
                    case AudioEntityType.Music:
                        addMusicToBufferAndPlay(ent);
                        break;
                    default:
                        break;
                }
            }
            //stop anything that we've been requested to stop, and dispose of it
            while (_tagsToStop.Count > 0) {
                string tag = _tagsToStop.Dequeue();
                if (_soundBuffer.ContainsKey(tag)) {
                    _soundBuffer[tag].Stop();
                    _soundBuffer[tag].Dispose();
                    _soundBuffer.Remove(tag);
                }
                if (_musicBuffer.ContainsKey(tag)) {
                    _musicBuffer[tag].Stop();
                    _musicBuffer[tag].Dispose();
                    _musicBuffer.Remove(tag);
                }
            }
        }

        private void addSoundToBufferAndPlay(IAudioEntity ent) {
            if (!_soundBuffer.ContainsKey(ent.tag)) {
                Sound snd = AudioMapper.getSound(ent.assetID);
                snd.Loop = ent.loop;
                snd.Volume = ent.volume;
                snd.Pitch = ent.pitch;
                _soundBuffer.Add(ent.tag, snd);
                _soundBuffer[ent.tag].Play();
            }
        }

        private void addMusicToBufferAndPlay(IAudioEntity ent) {
            if (!_musicBuffer.ContainsKey(ent.tag)) {
                Music msc = AudioMapper.getMusic(ent.assetID);
                msc.Loop = ent.loop;
                msc.Volume = ent.volume;
                msc.Pitch = ent.pitch;
                _musicBuffer.Add(ent.tag, msc);
                _musicBuffer[ent.tag].Play();
            }
        }

    }
}
