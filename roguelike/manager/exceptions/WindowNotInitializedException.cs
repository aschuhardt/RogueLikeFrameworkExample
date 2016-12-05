using System;

namespace roguelike.manager.exceptions {
    [Serializable]
    public class WindowNotInitializedException : Exception {
        public WindowNotInitializedException() { }
        public WindowNotInitializedException(string message) : base(message) { }
        public WindowNotInitializedException(string message, Exception inner) : base(message, inner) { }
        protected WindowNotInitializedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
