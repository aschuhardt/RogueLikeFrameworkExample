using roguelike.modules;
using System;

namespace roguelike {
    /// <summary>
    /// Provides a mapping between members of the State enum and implementations of the IModule interface.
    /// </summary>
    class StateMapper {
        private StateMapper() { }
        
        public static IModule TransitToState(string moduleName) {
            Type t = Type.GetType(moduleName);
            return (IModule)Activator.CreateInstance(t);
        }
    }
}
