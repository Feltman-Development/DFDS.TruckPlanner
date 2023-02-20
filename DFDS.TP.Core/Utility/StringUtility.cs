using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace DFDS.TP.Core.Utility;

public static class StringUtility
{
    #region Utilities

    /// <summary>
    /// Convert string into a typed value. Use to consistently convert strings in UI to a type, or as help in other parsing functions.
    /// If source string is null or empty, the default value of the target type (cast to an object) will be returned;
    /// </summary>
    /// <typeparam name="TTargetType"> Type to be converted to. </typeparam>
    /// <param name="sourceString"> Input string value to be converted. </param>
    /// <param name="culture"> Culture applied to conversion of dates. Default is CurrentCulture. </param>
    /// <returns> The string converted to the given type. </returns>
    public static TTargetType? StringToTypedValue<TTargetType>(string sourceString, CultureInfo? culture = null!) 
        => (TTargetType)StringToTypedValue(sourceString, typeof(TTargetType), culture)!;

    /// <summary>
    /// Convert string into a typed value. NOTE: Also available as '.ToTypedValue()' extension method on string.
    /// Explicitly assigns common types and falls back on using type converters for unhandled types.
    /// Use to consistently convert strings in UI to a type, or as help in other parsing functions.
    /// If source string is null or empty, the default value of the target type (cast to an object) will be returned;
    /// </summary>
    /// <param name="sourceString"> Input string value to be converted. </param>
    /// <param name="targetType"> Type to be converted to. </param>
    /// <param name="culture"> Culture applied to conversion of dates. Default is CurrentCulture. </param>
    /// <returns> The string converted to the given type, encapsulated in an object. </returns>
    public static object? StringToTypedValue(string sourceString, Type? targetType, CultureInfo? culture = null!)
    {
        if (targetType == null) return sourceString;
        if (string.IsNullOrEmpty(sourceString)) return Activator.CreateInstance(targetType);
        culture ??= CultureInfo.CurrentCulture;

        while (true)
        {
            if (targetType == typeof(string)) return sourceString;
            else if (targetType == typeof(int)) return int.Parse(sourceString, NumberStyles.Any, culture.NumberFormat);
            else if (targetType == typeof(long)) return long.Parse(sourceString, NumberStyles.Any, culture.NumberFormat);
            else if (targetType == typeof(short)) return short.Parse(sourceString, NumberStyles.Any, culture.NumberFormat);
            else if (targetType == typeof(decimal)) return decimal.Parse(sourceString, NumberStyles.Any, culture.NumberFormat);
            else if (targetType == typeof(DateTime)) return Convert.ToDateTime(sourceString, culture.DateTimeFormat);
            else if (targetType == typeof(byte)) return Convert.ToByte(sourceString);
            else if (targetType == typeof(double)) return double.Parse(sourceString, NumberStyles.Any, culture.NumberFormat);
            else if (targetType == typeof(float)) return float.Parse(sourceString, NumberStyles.Any, culture.NumberFormat);
            else if (targetType == typeof(bool)) return string.Equals(sourceString, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(sourceString, "on", StringComparison.OrdinalIgnoreCase) || sourceString == "1";
            else if (targetType == typeof(Guid)) return new Guid(sourceString);
            else if (targetType.IsEnum) return Enum.Parse(targetType, sourceString);
            else if (targetType == typeof(byte[])) return null!; // TODO: Convert HexBinary string to byte array
            
            else if (targetType.Name.StartsWith("Nullable`"))
            {
                if (sourceString?.ToLower() == "null" || sourceString?.Length == 0) return null!;
                else return StringToTypedValue(sourceString!, Nullable.GetUnderlyingType(targetType));
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertFrom(typeof(string))) return converter.ConvertFromString(null, culture, sourceString);
                
                Debug.Assert(false, $"Type Conversion not handled in StringToTypedValue for {targetType.Name} {sourceString}");
                throw new InvalidCastException("StringToTypedValueValueTypeConversionFailed" + targetType.Name);
            }
        }
    }

    /// <summary>
    /// Extracts a string from between a pair of delimiters. Only the first instance is found.
    /// </summary>
    public static string ExtractString(string source, string beginDelimiter, string endDelimiter, bool caseSensitive = false, bool allowMissingEndDelimiter = false, bool returnDelimiters = false)
    {
        int beginIndex, endIndex;
        if (string.IsNullOrEmpty(source)) return string.Empty;

        if (caseSensitive)
        {
            beginIndex = source.IndexOf(beginDelimiter, StringComparison.Ordinal);
            if (beginIndex == -1) return string.Empty;

            endIndex = !returnDelimiters ? source.IndexOf(endDelimiter, beginIndex + beginDelimiter.Length, StringComparison.Ordinal) : source.IndexOf(endDelimiter, beginIndex, StringComparison.Ordinal);
        }
        else
        {
            beginIndex = source.IndexOf(beginDelimiter, 0, source.Length, StringComparison.OrdinalIgnoreCase);
            if (beginIndex == -1) return string.Empty;

            endIndex = !returnDelimiters ? source.IndexOf(endDelimiter, beginIndex + beginDelimiter.Length, StringComparison.OrdinalIgnoreCase) : source.IndexOf(endDelimiter, beginIndex, StringComparison.OrdinalIgnoreCase);
        }

        if (allowMissingEndDelimiter && endIndex == -1) return source.Substring(beginIndex + beginDelimiter.Length);
        if (beginIndex > -1 && endIndex > 1)
        {
            return !returnDelimiters ? source.Substring(beginIndex + beginDelimiter.Length, endIndex - beginIndex - beginDelimiter.Length) : source.Substring(beginIndex, endIndex - beginIndex + endDelimiter.Length);
        }
        return string.Empty;
    }

    /// <summary>
    /// Replace a specific instance-number of a substring within a string.
    /// </summary>
    /// <returns> Updated string or original string if no matches. </returns>
    public static string ReplaceStringInstance(string originalString, string textToReplace, string textToReplaceWith, int instanceNumber, bool caseInsensitive)
    {
        if (instanceNumber == -1) return ReplaceString(originalString, textToReplace, textToReplaceWith, caseInsensitive);

        var index = 0;
        for (var i = 0; i < instanceNumber; i++)
        {
            index = caseInsensitive ? originalString.IndexOf(textToReplace, index, originalString.Length - index, StringComparison.OrdinalIgnoreCase) : originalString.IndexOf(textToReplace, index);

            if (index == -1) return originalString;
            else if (i < instanceNumber - 1) index += textToReplace.Length;
        }
        return string.Concat(originalString.AsSpan(0, index), textToReplaceWith, originalString.AsSpan(index + textToReplace.Length));
    }

    /// <summary>
    /// Replaces a substring within a string with another substring with optional case sensitivity turned off.
    /// </summary>
    /// <returns> Updated string or original string if no matches. </returns>
    public static string ReplaceString(string originalString, string partToReplace, string newStringPart, bool caseInsensitive)
    {
        var index = 0;
        while (true)
        {
            index = caseInsensitive ? originalString.IndexOf(partToReplace, index, originalString.Length - index, StringComparison.OrdinalIgnoreCase) : originalString.IndexOf(partToReplace, index, StringComparison.Ordinal);
            if (index == -1) break;

            originalString = string.Concat(originalString.AsSpan(0, index), newStringPart, originalString.AsSpan(index + partToReplace.Length));
            index += newStringPart.Length;
        }
        return originalString;
    }

    /// <summary>
    /// Returns the string with the given prefix removed. If the prefix isn't present, the original string is returned.
    /// </summary>
    public static string RemovePrefix(string value, string prefix) => value.StartsWith(prefix) ? value[prefix.Length..] : value;

    /// <summary>
    /// Returns the a substring taken from the right.
    /// </summary>
    public static string GetEndCharactersSubstring(string value, int numberOfCharacters) => value.Substring(value.Length - numberOfCharacters, numberOfCharacters);

    /// <summary>
    /// Get your text Base64Encoded.
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    #endregion Utilities


    /// <summary>
    /// Some of the utility methods from the class implemented as extension methods on <see cref="string"/>, to make life even easier for you and me.
    /// </summary>
    #region Extensions

    public static string[] SplitByLastOccurrence(this string input, string defaultValue, string prefix = "$")
    {
        var returnThis = new string[] { input, defaultValue };
        var prefixIndex = input.LastIndexOf(prefix, StringComparison.Ordinal);
        if (prefixIndex <= -1) return returnThis;

        returnThis[0] = input[..prefixIndex];
        returnThis[1] = input.Length > prefixIndex + 1 ? input[(prefixIndex + 1)..] : defaultValue; //+2 because of zero-based and we do not want the prefix character
        return returnThis;
    }

    public static string Truncate(this string value, int maxLength) => string.IsNullOrEmpty(value) ? value : value[..Math.Min(value.Length, maxLength)];

    /// <summary>
    /// Encode the string to base 64, with UTF8 pre-encoding.
    /// </summary>
    public static string AsBase64Encoded(this string value) => Base64Encode(value);

    /// <summary>
    /// Returns the a substring taken from the right.
    /// </summary>
    public static string EndCharactersSubstring(this string value, int numberOfCharacters) => value.Substring(value.Length - numberOfCharacters, numberOfCharacters);

    /// <summary>
    /// Strip characters from left of string
    /// </summary>
    public static string StrippedFromLeft(this string value, int numberOfCharacters) => value[numberOfCharacters..];

    /// <summary>
    /// Returns the string with the given prefix removed. If the prefix isn't present, the original string is returned.
    /// </summary>
    public static string WithoutPrefix(this string value, string prefix) => RemovePrefix(value, prefix);

    /// <summary>
    /// Automatic conversion of string into a typed value.
    /// Explicitly assigns common types and falls back on using type converters for unhandled types.
    /// Use to consistently convert strings in UI to a type, or as help in other parsing functions.
    /// </summary>
    /// <typeparam name="TTargetType">Type to be converted to</typeparam>
    /// <param name="sourceString">input string value to be converted</param>
    /// <param name="culture">Culture applied to conversion. Default is CurrentCulture. </param>
    public static TTargetType? AsTypedValue<TTargetType>(this string sourceString, CultureInfo? culture = null!) => StringToTypedValue<TTargetType>(sourceString, culture);

    #endregion Extensions
}