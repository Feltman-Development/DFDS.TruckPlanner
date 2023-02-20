namespace DFDS.TP.Core.Utility;

/// <summary>
/// Helper class for working with 'extended' enums using <see cref="StringValueAttribute"/> attributes.
/// There is both an instance implementation and a static implementation
/// </summary>
public class StringEnumUtility
{
    #region Instance implementation

    private static readonly Hashtable StringValues = new Hashtable();

    /// <summary>
    /// Creates a new <see cref="StringEnum"/> instance.
    /// </summary>
    /// <param name="enumType">Enum type.</param>
    public StringEnumUtility(Type enumType)
    {
        if (!enumType.IsEnum) throw new ArgumentException($"Supplied type must be an Enum.  Type was {enumType}");
        EnumType = enumType;
    }

    /// <summary>
    /// Gets the string value associated with the given enum value.
    /// </summary>
    /// <param name="valueName">Name of the enum value.</param>
    /// <returns>String Value</returns>
    public string GetStringValue(string valueName)
    {
        string stringValue = null;
        try
        {
            var enumType = (Enum)Enum.Parse(EnumType, valueName);
            stringValue = GetStringValue(enumType);
        }
        catch (Exception) { }//Swallow!

        return stringValue;
    }

    /// <summary>
    /// Gets the string values associated with the enum.
    /// </summary>
    /// <returns>String value array</returns>
    public Array GetStringValues()
    {
        var values = new ArrayList();
        foreach (var fi in EnumType.GetFields())
        {
            var attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attrs.Length > 0) values.Add(attrs[0].Value);
        }

        return values.ToArray();
    }

    /// <summary>
    /// Gets the values as a 'bindable' list datasource.
    /// </summary>
    /// <returns>IList for data binding</returns>
    public IList GetListValues()
    {
        var underlyingType = Enum.GetUnderlyingType(EnumType);
        var values = new ArrayList();
        foreach (var fi in EnumType.GetFields())
        {
            var attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attrs.Length > 0) values.Add(new DictionaryEntry(Convert.ChangeType(Enum.Parse(EnumType, fi.Name), underlyingType), attrs[0].Value));
        }
        return values;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>
    /// <param name="stringValue">String value.</param>
    /// <returns>Existence of the string value</returns>
    public bool IsStringDefined(string stringValue)
    {
        return Parse(EnumType, stringValue) != null;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>
    /// <param name="stringValue">String value.</param>
    /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
    /// <returns>Existence of the string value</returns>
    public bool IsStringDefined(string stringValue, bool ignoreCase)
    {
        return Parse(EnumType, stringValue, ignoreCase) != null;
    }

    /// <summary>
    /// Gets the underlying enum type for this instance.
    /// </summary>
    /// <value></value>
    public Type EnumType { get; private set; }

    #endregion

    #region Static implementation

    /// <summary>
    /// Gets a string value for a particular enum value.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <returns>String Value associated via a <see cref="StringValueAttribute"/> attribute, or null if not found.</returns>
    public static string GetStringValue(Enum value)
    {
        string output = null;
        var type = value.GetType();

        if (StringValues.ContainsKey(value)) output = ((StringValueAttribute)StringValues[value]).Value;
        else
        {
            var fi = type.GetField(value.ToString());
            var attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attrs.Length <= 0) return output;

            StringValues.Add(value, attrs[0]);
            output = attrs[0].Value;
        }
        return output;
    }

    /// <summary>
    /// Gets a enum value for a particular StringValueAttributevalue.
    /// </summary>
    /// <param name="value">value.</param>
    /// <returns>Enum value associated via a <see cref="StringValueAttribute"/> attribute, or null if not found.</returns>
    public static T GetEnumValue<T>(string value)
    {
        if (!typeof(T).IsEnum) { throw new ArgumentException($"Method can only be called with an enum type. Type was {typeof(T)}"); }

        foreach (var enumValue in typeof(T).GetFields())
        {
            if (!enumValue.IsStatic) continue;
            var attributter = enumValue.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            var enumStringBaseValue = attributter != null && attributter.Length > 0 ? attributter[0].Value : null;
            if (enumStringBaseValue == value) { return (T)Enum.Parse(typeof(T), enumValue.Name); }
        }
        return default;
    }

    /// <summary>
    /// Parses the supplied enum and string value to find an associated enum value.
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="stringValue">String value.</param>
    /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
    /// <returns>Enum value associated with the string value, or null if not found.</returns>
    public static object Parse(Type type, string stringValue, bool ignoreCase = false)
    {
        object output = null;
        string enumStringValue = null;

        if (!type.IsEnum) throw new ArgumentException($"Supplied type must be an Enum. Type was {type}");

        foreach (var fi in type.GetFields())
        {
            var attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attrs.Length > 0) enumStringValue = attrs[0].Value;
            if (string.Compare(enumStringValue, stringValue, ignoreCase) != 0) continue;

            output = Enum.Parse(type, fi.Name);
            break;
        }

        return output;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>
    /// <param name="stringValue">String value.</param>
    /// <param name="enumType">Type of enum</param>
    /// <returns>Existence of the string value</returns>
    public static bool IsStringDefined(Type enumType, string stringValue)
    {
        return Parse(enumType, stringValue) != null;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>
    /// <param name="stringValue">String value.</param>
    /// <param name="enumType">Type of enum</param>
    /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
    /// <returns>Existence of the string value</returns>
    public static bool IsStringDefined(Type enumType, string stringValue, bool ignoreCase)
    {
        return Parse(enumType, stringValue, ignoreCase) != null;
    }

    public static IList<string> GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList().Select(v => v.ToString()).ToList();
    }

    public static IList<string> GetStringValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<Enum>().Select(GetStringValue).ToList();
    }

    #endregion
}
