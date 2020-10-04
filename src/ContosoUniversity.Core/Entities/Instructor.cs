using ContosoUniversity.Core.Interfaces;

namespace ContosoUniversity.Core.Entities
{
    public class Instructor : BaseEntity, IAggregateRoot
    {
        public Instructor()
        {
            
        }
    }
}