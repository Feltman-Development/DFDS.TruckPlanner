namespace DFDS.TP.Core.Utility;

/// <summary>
/// Attribute that allows enums to have a string value. See class <see cref="StringEnumUtility"/> for handling of this.
/// </summary>
[AttributeUsage(AttributeTargets.Enum)]
public class StringValueAttribute : Attribute
{
    /// <inheritdoc />
    public StringValueAttribute(string value) => Value = value;

    public string Value { get; }
}