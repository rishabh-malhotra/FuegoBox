using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.Exceptions
{
    public class UserNameDoesNotExistsException:Exception
    {
        public UserNameDoesNotExistsException() { }
        public UserNameDoesNotExistsException(string message) : base(message) { }
        public UserNameDoesNotExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
