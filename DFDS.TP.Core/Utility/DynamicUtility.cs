namespace DFDS.TP.Core.Utility;

//TODO: Needs C#10 refactoring!
/// <summary>
/// Some methods to assist handling dynamic types, like ExpandoObject
/// </summary>
public static class DynamicUtility
{
    public static object GetTypedObject(dynamic input)
    {
        if (input is not ExpandoObject) return input;

        var type = CreateAbstractClassType(input);
        return CreateObject(type, input);
    }

    public static Type CreateAbstractClassType(dynamic input)
    {
        if (input == null) return typeof(object);
        if (input is not ExpandoObject expandoObject) return input.GetType();

        var properties = new List<DynamicProperty>();
        foreach (var expando in expandoObject)
        {
            Type value;
            if (expando.Value is IList list)
            {
                if (list.Count == 0)
                {
                    value = typeof(List<object>);
                }
                else
                {
                    var internalType = CreateAbstractClassType(list[0]!);
                    value = new List<object>().Cast(internalType).ToList(internalType).GetType();
                }
            }
            else
            {
                value = CreateAbstractClassType(expando.Value);
            }
            properties.Add(new DynamicProperty(expando.Key, value));
        }
        return DynamicClassFactory.CreateType(properties);
    }

    public static object CreateObject(Type type, dynamic input)
    {
        if (!(input is ExpandoObject expandoObject)) return Convert.ChangeType(input, type);

        var obj = Activator.CreateInstance(type)!;
        var typeProps = type.GetProperties().ToDictionary(c => c.Name);

        foreach (var expando in expandoObject)
        {
            if (!typeProps.ContainsKey(expando.Key) || expando.Value == null || expando.Value.GetType().Name == "DBNull" && expando.Value == DBNull.Value) continue;

            object value;
            var propertyInfo = typeProps[expando.Key];
            if (expando.Value is ExpandoObject)
            {
                var propType = propertyInfo.PropertyType;
                value = CreateObject(propType, expando.Value);
            }
            else if (expando.Value is IList list)
            {
                var internalType = propertyInfo.PropertyType.GenericTypeArguments.FirstOrDefault() ?? typeof(object);
                var newList = new List<object>().Cast(internalType).ToList(internalType);
                foreach (var t in list)
                {
                    var child = CreateObject(internalType, t);
                    newList.Add(child);
                };
                value = newList;
            }
            else
            {
                value = expando.Value;
            }
            propertyInfo.SetValue(obj, value, null);
        }
        return obj;
    }

    private static IEnumerable? Cast(this IEnumerable self, Type innerType)
    {
        var methodInfo = typeof(Enumerable).GetMethod("Cast");
        var genericMethod = methodInfo?.MakeGenericMethod(innerType);
        return genericMethod?.Invoke(null, new object?[] { self }) as IEnumerable;
    }

    private static IList? ToList(this IEnumerable? self, Type innerType)
    {
        var methodInfo = typeof(Enumerable).GetMethod("ToList");
        var genericMethod = methodInfo?.MakeGenericMethod(innerType);
        return genericMethod?.Invoke(null, new object?[] { self }) as IList;
    }
}
