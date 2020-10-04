using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversity.Core.Exceptions
{
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException(string message) : base(message)
        {
            
        }
    }
}
