using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using ContosoUniversity.Core.Exceptions;
using ContosoUniversity.Core.Interfaces;

namespace ContosoUniversity.Core.Entities.CourseAggregate
{
    public class Course : BaseEntity, IAggregateRoot
    {
        public int DepartmentId { get; private set; }
        public const int TitleMaxLength = 100;
        private readonly List<Enrollment> _enrollments = new List<Enrollment>();

        public Course()
        {
            // used for EF
        }

        public Course(string title, int credits, int departmentId)
        {
            DepartmentId = departmentId;
            SetTitle(title);
            SetCredits(credits);
        }

        public string Title { get; private set; }
        public int Credits { get; private set; }
        public bool Active { get; private set; } = true;
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;

        public void Activate()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }

        public void DropStudent(int studentId)
        {
            var enrollment = _enrollments.FirstOrDefault(e => e.StudentId == studentId);

            if (enrollment == null)
                throw new StudentNotFoundException(ExceptionMessages.StudentEnrollmentNotFound(studentId));

            if (enrollment.Grade != null)
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
            Guard.Against.Null(grade, nameof(grade));

            var enrollment = _enrollments.FirstOrDefault(e => e.StudentId == studentId);

            if (enrollment == null)
                throw new StudentNotFoundException(ExceptionMessages.StudentEnrollmentNotFound(studentId));

            enrollment.UpdateGrade(grade);
        }

        public void UpdateDetails(string title, int credits, int departmentId)
        {
            DepartmentId = departmentId;
            SetTitle(title);
            SetCredits(credits);
        }

        private void SetCredits(int credits)
        {
            Guard.Against.NegativeOrZero(credits, nameof(credits));

            Credits = credits;
        }

        private void SetTitle(string title)
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.OutOfRange(title.Length, nameof(title), 1, TitleMaxLength);

            Title = title;
        }
    }
}