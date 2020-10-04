using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Core.Exceptions;
using ContosoUniversity.Core.Interfaces;

namespace ContosoUniversity.Core.Entities.CourseAggregate
{
    public class Course : BaseEntity, IAggregateRoot
    {
        private readonly List<Enrollment> _enrollments = new List<Enrollment>();

        public Course()
        {
            // used for EF
        }

        public Course(string title, int credits, bool active = true)
        {
            Title = title;
            Credits = credits;
            Active = active;
        }

        public string Title { get; }
        public int Credits { get; }
        public bool Active { get; }
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;

        public void DropStudent(int studentId)
        {
            var enrollment = _enrollments.FirstOrDefault(e => e.StudentId == studentId);

            if (enrollment?.Grade == null)
                throw new StudentEnrollmentException(ExceptionMessages.DropStudentWithGrade);

            _enrollments.Remove(enrollment);
        }

        public void EnrollStudent(int studentId)
        {
            if (!Active) throw new StudentEnrollmentException(ExceptionMessages.EnrollToInactiveCourse);

            if (_enrollments.Any(e => e.StudentId == studentId))
                throw new StudentEnrollmentException(ExceptionMessages.StudentAlreadyEnrolled(studentId));

            var enrollment = new Enrollment(studentId, null);
            _enrollments.Add(enrollment);
        }

        public void GradeStudent(int studentId, Grade grade)
        {
            var enrollment = _enrollments.FirstOrDefault(e => e.StudentId == studentId);

            if (enrollment == null)
                throw new StudentNotFoundException(ExceptionMessages.StudentEnrollmentNotFound(studentId)); 

            enrollment.UpdateGrade(grade);
        }
    }
}