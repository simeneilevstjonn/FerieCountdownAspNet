using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Exceptions
{
    public class InvalidLocaleException : Exception
    {
        public InvalidLocaleException()
        {
        }

        public InvalidLocaleException(string message)
            : base(message)
        {
        }

        public InvalidLocaleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
