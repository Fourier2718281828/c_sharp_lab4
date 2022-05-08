using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exceptions
{
    internal class BadEmailException : Exception
    {
        public BadEmailException(string message)
            : base($"BadEmailException: {message}")
        {
            return;
        }
    }
}
