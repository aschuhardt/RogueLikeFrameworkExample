using System;

namespace RoguePanda.asset {
    [Serializable]
    public class UnknownFileTypeException : Exception {
        public UnknownFileTypeException() { }
        public UnknownFileTypeException(string message) : base(message) { }
        public UnknownFileTypeException(string message, Exception inner) : base(message, inner) { }
        protected UnknownFileTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
