using System;

namespace RoguePanda.asset {
    [Serializable]
    public class IncorrectAssetTypeException : Exception {
        public IncorrectAssetTypeException() { }
        public IncorrectAssetTypeException(string message) : base(message) { }
        public IncorrectAssetTypeException(string message, Exception inner) : base(message, inner) { }
        protected IncorrectAssetTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
