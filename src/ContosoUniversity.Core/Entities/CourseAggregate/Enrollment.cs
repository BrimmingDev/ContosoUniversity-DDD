#nullable enable
namespace ContosoUniversity.Core.Entities.CourseAggregate
{
    public class Enrollment : BaseEntity
    {
        public Enrollment(int studentId, Grade? grade)
        {
            StudentId = studentId;
            Grade = grade;
        }

        public int StudentId { get; }
        public Grade? Grade { get; private set; }

        public void UpdateGrade(Grade grade)
        {
            Grade = grade;
        }
    }
}