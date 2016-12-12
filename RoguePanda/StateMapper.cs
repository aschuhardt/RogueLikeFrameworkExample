using RoguePanda;
using RoguePanda.manager;
using RoguePanda.modules;
using System;
using System.Reflection;

namespace RoguePanda {
    /// <summary>
    /// Provides a mapping between members of the State enum and implementations of the IModule interface.
    /// </summary>
    class StateMapper {
        private StateMapper() { }
        
        public static IModule TransitToState(string moduleName) {
            try {
                Type t = Assembly.GetEntryAssembly().GetType(moduleName);
                return (IModule)Activator.CreateInstance(t);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
