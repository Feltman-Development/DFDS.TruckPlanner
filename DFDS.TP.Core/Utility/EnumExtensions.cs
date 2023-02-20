using System.ComponentModel;

namespace DFDS.TP.Core.Utility;

public static class EnumExtensions
{
    /// <summary>
    /// Extension to retrieve a canonical name for an enumeration value. 
    /// The name is specified, in the enumeration, using the "Name" attribute.
    /// </summary>
    public static string GetDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());
        var attributes = fi != null ? (DescriptionAttribute[]?) fi.GetCustomAttributes(typeof(DescriptionAttribute), false) : null;
        return attributes is {Length: > 0} ? attributes[0].Description : value.ToString();
    }

    public static T Parse<T>(string value) => (T)Enum.Parse(typeof(T), value);

    public static IEnumerable<T> GetValues<T>() => Enum.GetValues(typeof(T)).Cast<T>();
}