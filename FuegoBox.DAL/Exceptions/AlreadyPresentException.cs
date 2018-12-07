using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.Exceptions
{
    public class AlreadyPresentException:Exception
    {
        public AlreadyPresentException() { }
        public AlreadyPresentException(string message) : base(message) { }
        public AlreadyPresentException(string message, Exception inner) : base(message, inner) { } 
    }
}
