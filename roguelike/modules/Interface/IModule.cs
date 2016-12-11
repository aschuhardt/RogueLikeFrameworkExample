using roguelike.entity;
using System.Collections.Generic;

namespace roguelike.modules {
    interface IModule {
        bool closing { get; }
        string moduleState { get; }
        string nextStateType { get; }
        IEnumerable<IEntity> entities { get; }
        IList<object> transferParameters { get; }
        bool shouldReInitializeWindow { get; }
        VideoSettings videoSettings { get; set; }
        bool init(IList<object> parameters);
        void run();
        void setInput(InputType input);
        string keyPressed { get; set; }
    }
}
