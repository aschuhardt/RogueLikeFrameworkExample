using RoguePanda.modules;
using System;

namespace RoguePanda {
    /// <summary>
    /// Provides a mapping between members of the State enum and implementations of the IModule interface.
    /// </summary>
    class StateMapper {
        private StateMapper() { }
        
        public static IModule TransitToState(string moduleName) {
            try {
                Type t = Type.GetType(moduleName);
                return (IModule)Activator.CreateInstance(t);
            } catch (Exception) {
                Console.WriteLine("Tried to create an instance of a module that was not found.  Try correcting the fully-qualified name of the class to be called.");
                throw;
            }
        }
    }
}
