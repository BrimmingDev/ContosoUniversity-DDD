namespace ContosoUniversity.Core.Exceptions
{
    public static class ExceptionMessages
    {
        public const string EnrollToInactiveCourse = "Cannot enroll student to an Inactive course.";
        public const string DropStudentWithGrade = "Cannot drop student who received a grade for the course.";

        public static string StudentAlreadyEnrolled(int studentId)
        {
            return $"Student with id '{studentId}' already enrolled in this course for the provide semester.";
        }

        public static string StudentEnrollmentNotFound(int studentId)
        {
            return $"Student enrollment record for student id '{studentId}' was not found.";

        }
    }
}