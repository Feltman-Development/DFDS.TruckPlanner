namespace DFDS.TP.Domain.Base;

public class ValueObject<TValue> : IEquatable<TValue>, IValueObject<TValue> where TValue : ValueObject<TValue>
{
    /// <summary>
    /// Returns true objects are same type and all fields have same value
    /// </summary>
    public override bool Equals([AllowNull] object otherObject) => otherObject != null && Equals(otherObject as TValue);

    /// <summary>
    /// Returns true if all fields on object have same value
    /// </summary>
    public virtual bool Equals([AllowNull] TValue otherValue)
    {
        if (otherValue is null) return false;

        Type thisType = GetType();
        Type otherType = otherValue.GetType();
        if (thisType != otherType) return false;

        foreach (FieldInfo field in thisType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            object? value1 = field.GetValue(otherValue);
            object? value2 = field.GetValue(this);
            if (value1 == null && value2 != null) return false;
            if (!value1.Equals(value2)) return false;
        }
        return true;
    }

    /// <summary>
    /// Returns hashcode based on multiplier on all value fields
    /// </summary>
    public override int GetHashCode()
    {
        IEnumerable<FieldInfo> fields = GetFields();
        const int startValue = 17; //TODO: Move number!
        const int multiplier = 59; //TODO: Move number!
        int hashCode = startValue;

        foreach (FieldInfo field in fields)
        {
            object? value = field.GetValue(this);
            if (value != null) hashCode = hashCode * multiplier + value.GetHashCode();
        }
        return hashCode;
    }

    private IEnumerable<FieldInfo> GetFields()
    {
        var thisType = GetType();
        var fields = new List<FieldInfo>();
        while (thisType != null && thisType != typeof(object))
        {
            fields.AddRange(thisType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
            thisType = thisType.BaseType;
        }
        return fields;
    }

    /// <summary>
    /// Makes a deep compare (see 'equals')
    /// </summary>
    public static bool operator ==(ValueObject<TValue> x, ValueObject<TValue> y) => x.Equals(y);

    /// <summary>
    /// Makes a deep compare (see 'equals')
    /// </summary>
    public static bool operator !=(ValueObject<TValue> x, ValueObject<TValue> y) => !(x == y);
}
