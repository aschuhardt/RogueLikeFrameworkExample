using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePanda.drawobject.color {
    [Serializable]
    public class InvalidEntityColorValueException : Exception {
        public InvalidEntityColorValueException() { }
        public InvalidEntityColorValueException(string message) : base(message) { }
        public InvalidEntityColorValueException(string message, Exception inner) : base(message, inner) { }
        protected InvalidEntityColorValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
