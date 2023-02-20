namespace DFDS.TP.Domain.Base;

public interface IValueObject<TValue> where TValue : IValueObject<TValue>
{
    bool Equals(object? otherObject);

    bool Equals(TValue? otherValue);

    int GetHashCode();
}