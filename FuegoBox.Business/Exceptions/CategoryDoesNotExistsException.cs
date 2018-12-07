using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.Exceptions
{
    public class CategoryDoesNotExistsException:Exception
    {
        public CategoryDoesNotExistsException() { }
        public CategoryDoesNotExistsException(string message) : base(message) { }
        public CategoryDoesNotExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
