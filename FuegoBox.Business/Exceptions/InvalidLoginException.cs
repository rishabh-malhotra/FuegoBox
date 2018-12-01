using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.Exceptions
{
    public class InvalidLoginException:Exception
    {
        public InvalidLoginException()
        {

        }
        public InvalidLoginException(string message): base(message)
        {

        }
        public InvalidLoginException(string message,Exception inner):base(message,inner)
        {
                
        }
    }
}
