using System.Collections.Generic;
using Ardalis.GuardClauses;

namespace ContosoUniversity.Core.Entities.StudentAggregate
{
    public class PersonName : ValueObject
    {
        public PersonName(string firstName, string lastName, string nickName = null)
        {
            Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
            Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string NickName { get; }

        public string PreferredName => NickName.Length > 0 ? NickName : FirstName;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return NickName;
        }
    }
}