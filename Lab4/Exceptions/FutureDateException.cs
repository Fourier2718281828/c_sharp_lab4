using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exceptions
{
    internal class FutureDateException : Exception
    {
        public FutureDateException(string message)
            : base($"FutureDateException: {message}")
        {
            return;
        }
    }
}
