using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.asset {
    [Serializable]
    public class InvalidAssetStreamAccessException : Exception {
        public InvalidAssetStreamAccessException() { }
        public InvalidAssetStreamAccessException(string message) : base(message) { }
        public InvalidAssetStreamAccessException(string message, Exception inner) : base(message, inner) { }
        protected InvalidAssetStreamAccessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
