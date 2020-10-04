using System;

namespace ContosoUniversity.Core.Exceptions
{
    public class StudentEnrollmentException : Exception
    {
        public StudentEnrollmentException(string message) : base(message)
        {
        }
    }
}