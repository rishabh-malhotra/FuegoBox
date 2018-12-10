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
        public UserNameAlreadyExistsException(string message) : base("This email has already exists.") { }
        public UserNameAlreadyExistsException(string message,Exception inner):base(message,inner)
        {}
    }
}
