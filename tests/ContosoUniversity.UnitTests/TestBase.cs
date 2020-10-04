using AutoFixture;

namespace ContosoUniversity.UnitTests
{
    public class TestBase
    {
        public TestBase()
        {
            Fixture = new Fixture();
        }

        public Fixture Fixture { get; }
    }
}