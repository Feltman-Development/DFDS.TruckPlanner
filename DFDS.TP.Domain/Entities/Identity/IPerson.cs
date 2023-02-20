using DFDS.TP.Domain.Base;
using DFDS.TP.Domain.Entities.Details;

namespace DFDS.TP.Domain.Entities.Identity;

public interface IPerson : IEntity
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string FullName { get; }

    int Age { get; }

    DateOnly BirthDate { get; }

    IEnumerable<PhoneNumber> PhoneNumbers { get; }

    IEnumerable<Address> Addresses { get; }

    IEnumerable<Email> Emails { get; }
}