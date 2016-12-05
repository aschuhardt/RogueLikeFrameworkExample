using roguelike.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike.modules {
    interface IModule {
        bool closing { get; }
        State moduleState { get; }
        State nextStateType { get; }
        IEnumerable<IEntity> entities { get; }
        IList<object> transferParameters { get; }
        bool shouldReInitializeWindow { get; }
        VideoSettings videoSettings { get; set; }
        bool init(IList<object> parameters);
        void run();
        void setInput(InputType input);
    }
}
