using RoguePanda.entity;
using System.Collections.Generic;

namespace RoguePanda.modules {
    internal interface IModule {
        bool closing { get; }
        string moduleState { get; }
        string nextStateType { get; }
        IEnumerable<ITextEntity> textEntities { get; }
        IEnumerable<ISpriteEntity> spriteEntities { get; }
        IEnumerable<IAudioEntity> audioEntities { get; }
        IEnumerable<string> requestedAudioStopTags { get; }
        IList<object> transferParameters { get; }
        bool shouldReInitializeWindow { get; }
        VideoSettings videoSettings { get; set; }
        bool init(IList<object> parameters);
        void run();
        void setInput(InputType input);
        string keyPressed { get; set; }
    }
}
