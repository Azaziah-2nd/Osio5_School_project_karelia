using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osio5
{
    class Error_class : Exception
    {
        public Error_class()
        {
        }
        public Error_class(string message) 
        : base(message)
        {
        }

    }
}
