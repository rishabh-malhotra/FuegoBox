using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.Exceptions
{
    public class CategoryNotFoundException:Exception
    {
        public CategoryNotFoundException() { }
        public CategoryNotFoundException(string message) : base(message) { }
        public CategoryNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
