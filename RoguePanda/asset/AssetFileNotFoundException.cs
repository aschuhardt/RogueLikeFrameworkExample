using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.asset {
    [Serializable]
    public class AssetFileNotFoundException : Exception {
        public AssetFileNotFoundException() { }
        public AssetFileNotFoundException(string message) : base(message) { }
        public AssetFileNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected AssetFileNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
