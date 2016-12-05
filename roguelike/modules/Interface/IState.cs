using roguelike.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike.modules {
    interface IState {
        bool closing { get; }
        State moduleState { get; }
        State nextStateType { get; }
        IEnumerable<IEntity> entities { get; }
        IList<object> transferParameters { get; }
        bool init(IList<object> parameters);
        void run();
        void setInput(InputType input);
    }
}
