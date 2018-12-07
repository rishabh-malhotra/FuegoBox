using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.Exceptions
{
   public class ItemAlreadyInCartException:Exception
    {
        public ItemAlreadyInCartException() { }
        public ItemAlreadyInCartException(string message) : base(message) { }
        public ItemAlreadyInCartException(string message, Exception inner) : base(message, inner) { }
    }
}
