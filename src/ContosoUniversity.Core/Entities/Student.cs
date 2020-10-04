using System;
using ContosoUniversity.Core.Interfaces;

namespace ContosoUniversity.Core.Entities
{
    public class Student : BaseEntity, IAggregateRoot
    {
        public Student(PersonName name, IDateTime dateTime)
        {
            Name = name;
            EnrollmentDate = dateTime.Now;
        }

        public PersonName Name { get; }
        public DateTime EnrollmentDate { get; }
    }
}