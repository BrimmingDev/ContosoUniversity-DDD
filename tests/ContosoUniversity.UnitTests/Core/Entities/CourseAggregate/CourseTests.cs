using System;
using System.Linq;
using Ardalis.SmartEnum.AutoFixture;
using AutoFixture;
using AutoFixture.Xunit2;
using ContosoUniversity.Core.Entities.CourseAggregate;
using ContosoUniversity.Core.Exceptions;
using Shouldly;
using Xunit;

namespace ContosoUniversity.UnitTests.Core.Entities.CourseAggregate
{
    public class CourseTests : TestBase
    {
        public CourseTests()
        {
            Fixture.Customizations.Add(new SmartEnumSpecimenBuilder());
        }

        [Theory]
        [AutoData]
        public void ShouldThrowArgumentExceptionGivenNullTitle(int credits)
        {
            Should.Throw<ArgumentException>(() => new Course(" ", credits));
        }

        [Theory]
        [AutoData]
        public void ShouldThrowArgumentNullExceptionGivenNullTitle(int credits)
        {
            Should.Throw<ArgumentNullException>(() => new Course(null, credits));
        }

        [Theory]
        [AutoData]
        public void ShouldThrowArgumentOutOfRangeExceptionGivenTitleTooLong(int credits)
        {
            var longString = string.Join(string.Empty, Fixture.CreateMany<char>(Course.TitleMaxLength + 1));

            Should.Throw<ArgumentOutOfRangeException>(() => new Course(longString, credits));
        }

        [Theory]
        [InlineAutoData(0)]
        [InlineAutoData(-1)]
        public void ShouldThrowArgumentExceptionGivenNegativeOrZeroCredits(int credits, string title)
        {
            Should.Throw<ArgumentException>(() => new Course(title, credits));
        }

        [Theory]
        [AutoData]
        public void ShouldCreateCourseGivenValidParameters(string title, int credits)
        {
            var course = new Course(title, credits);

            course.Active.ShouldBeTrue();
            course.Title.ShouldBe(title);
            course.Credits.ShouldBe(credits);
        }

        [Theory]
        [AutoData]
        public void Deactivate_ShouldDeactivateCourse(Course sut)
        {
            sut.Deactivate();

            sut.Active.ShouldBeFalse();
        }

        [Theory]
        [AutoData]
        public void Activate_ShouldActivateCourse(Course sut)
        {
            sut.Deactivate();
            sut.Active.ShouldBeFalse();

            sut.Activate();

            sut.Active.ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void EnrollStudent_ShouldThrowStudentEnrollmentExceptionGivenInactiveCourse(Course sut, int studentId)
        {
            sut.Deactivate();

            Should.Throw<StudentEnrollmentException>(() => sut.EnrollStudent(studentId)).Message
                .ShouldBe(ExceptionMessages.EnrollToInactiveCourse);
        }

        [Theory]
        [AutoData]
        public void EnrollStudent_ShouldThrowStudentEnrollmentExceptionGivenStudentAlreadyEnrolled(Course sut,
            int studentId)
        {
            sut.EnrollStudent(studentId);

            Should.Throw<StudentEnrollmentException>(() => sut.EnrollStudent(studentId)).Message
                .ShouldBe(ExceptionMessages.StudentAlreadyEnrolled(studentId));
        }

        [Theory]
        [AutoData]
        public void EnrollStudent_ShouldEnrollStudent(Course sut, int studentId)
        {
            sut.EnrollStudent(studentId);

            sut.Enrollments.ShouldContain(x => x.StudentId == studentId);
        }

        [Theory]
        [AutoData]
        public void GradeStudent_ShouldThrowArgumentNullExceptionGivenNullGrade(Course sut, int studentId)
        {
            Should.Throw<ArgumentNullException>(() => sut.GradeStudent(studentId, null));
        }

        [Theory]
        [AutoData]
        public void GradeStudent_ShouldThrowStudentNotFoundExceptionGivenInvalidStudentId(Course sut, int studentId)
        {
            var grade = Fixture.Create<Grade>();

            Should.Throw<StudentNotFoundException>(() => sut.GradeStudent(studentId, grade));
        }

        [Theory]
        [AutoData]
        public void GradeStudent_ShouldGradeStudent(Course sut, int studentId)
        {
            var grade = Fixture.Create<Grade>();
            sut.EnrollStudent(studentId);

            sut.GradeStudent(studentId, grade);

            sut.Enrollments.First(x => x.StudentId == studentId).Grade.ShouldBe(grade);
        }

        [Theory]
        [AutoData]
        public void DropStudent_ShouldThrowStudentNotFoundExceptionGivenInvalidStudentId(Course sut, int studentId)
        {
            Should.Throw<StudentNotFoundException>(() => sut.DropStudent(studentId)).Message
                .ShouldBe(ExceptionMessages.StudentEnrollmentNotFound(studentId));
        }

        [Theory]
        [AutoData]
        public void DropStudent_ShouldThrowStudentEnrollmentExceptionGivenStudentWithGrade(Course sut, int studentId)
        {
            var grade = Fixture.Create<Grade>();
            sut.EnrollStudent(studentId);
            sut.GradeStudent(studentId, grade);

            Should.Throw<StudentEnrollmentException>(() => sut.DropStudent(studentId)).Message
                .ShouldBe(ExceptionMessages.DropStudentWithGrade);
        }

        [Theory]
        [AutoData]
        public void DropStudent_ShouldDropStudentGivenValidParameters(Course sut, int studentId)
        {
            sut.EnrollStudent(studentId);

            sut.DropStudent(studentId);

            sut.Enrollments.ShouldNotContain(x => x.StudentId == studentId);
        }

        [Theory]
        [AutoData]
        public void UpdateDetails_ShouldUpdateCourseDetails(Course sut, string updatedTitle, int updatedCredits)
        {
            sut.UpdateDetails(updatedTitle, updatedCredits);

            sut.Title.ShouldBe(updatedTitle);
            sut.Credits.ShouldBe(updatedCredits);
        }
    }
}