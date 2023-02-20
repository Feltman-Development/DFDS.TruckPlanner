namespace DFDS.TP.Domain.Entities.Details;

public record PhoneNumber(PhoneType PhoneType, string PhoneName, string CountryCodeText, int CountryCodeNumber, int Number, string extension = "")
{

}

public class PhoneNumbers : List<PhoneNumber> { }