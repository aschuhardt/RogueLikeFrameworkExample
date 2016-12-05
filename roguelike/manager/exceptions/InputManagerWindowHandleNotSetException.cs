using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roguelike.manager.exceptions {
    [Serializable]
    public class InputManagerWindowHandleNotSetException : Exception {
        public InputManagerWindowHandleNotSetException() { }
        public InputManagerWindowHandleNotSetException(string message) : base(message) { }
        public InputManagerWindowHandleNotSetException(string message, Exception inner) : base(message, inner) { }
        protected InputManagerWindowHandleNotSetException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
