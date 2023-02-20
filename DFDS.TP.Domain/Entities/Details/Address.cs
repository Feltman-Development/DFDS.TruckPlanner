namespace DFDS.TP.Domain.Entities.Details;

public record Address(AddressType AddressType, string Name, string Description, string AddressLine1, string AddressLine2, string HouseNumber, string FloorNumber, string City, string PostalCode, string CountyRegionMunicipality, string CountryOrState);


public class Addresses : List<Address>
{
    public Addresses(IEnumerable<Address> collection) : base(collection)
    {
    }

    public Addresses(params Address[] addresses)
    {
        foreach (var address in addresses)
        {
            Add(address);
        }
    }

    public void AddAddress(Address address) => Add(address);
}