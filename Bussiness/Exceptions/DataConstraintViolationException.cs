using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Exceptions
{
    public class DataConstraintViolationException : Exception
    {
        public DataConstraintViolationException(string message) : base(message)
        {
        }
    }
}
