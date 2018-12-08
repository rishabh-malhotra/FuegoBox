using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.Exceptions
{
   public class UserNameAlreadyExistsException :Exception
    {
        public UserNameAlreadyExistsException() { } 
        public UserNameAlreadyExistsException(string message) : base(message) { }
        public UserNameAlreadyExistsException(string message,Exception inner):base(message,inner)
        {}
    }
}
