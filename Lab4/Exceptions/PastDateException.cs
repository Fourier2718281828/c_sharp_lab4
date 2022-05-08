using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Exceptions
{
    internal class PastDateException : Exception
    {
        public PastDateException(string message)
            : base($"PastDateException: {message}")
        {
            return;
        }
    }
}
