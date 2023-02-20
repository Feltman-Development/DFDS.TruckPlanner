namespace FDEV.ISDDB.Infrastructure.EFCore.Config;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
internal sealed class NotNullAttribute : Attribute { }

[AttributeUsage( AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
internal sealed class CanBeNullAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class InvokerParameterNameAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Parameter)]
internal sealed class NoEnumerationAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
internal sealed class ContractAnnotationAttribute : Attribute
{
    public string Contract { get; }

    public bool ForceFullStates { get; }

    public ContractAnnotationAttribute([NotNull] string contract) : this(contract, false) { }

    public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
    {
        Contract = contract;
        ForceFullStates = forceFullStates;
    }
}