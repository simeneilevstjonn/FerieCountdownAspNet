using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Exceptions
{
    public class BadSqlException : Exception
    {
        public BadSqlException()
        {
        }

        public BadSqlException(string message)
            : base(message)
        {
        }

        public BadSqlException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
