using System;
using AutoFixture;
using AutoFixture.Xunit2;
using ContosoUniversity.Core.Entities.CourseAggregate;
using Shouldly;
using Xunit;

namespace ContosoUniversity.UnitTests.Core.Entities.CourseAggregate
{
    public class CourseTests : TestBase
    {
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
    }
}