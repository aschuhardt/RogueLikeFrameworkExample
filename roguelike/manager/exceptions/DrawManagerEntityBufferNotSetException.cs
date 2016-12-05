using System;

namespace roguelike.manager.exceptions {
    [Serializable]
    public class DrawManagerEntityBufferNotSetException : Exception {
        public DrawManagerEntityBufferNotSetException() { }
        public DrawManagerEntityBufferNotSetException(string message) : base(message) { }
        public DrawManagerEntityBufferNotSetException(string message, Exception inner) : base(message, inner) { }
        protected DrawManagerEntityBufferNotSetException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
