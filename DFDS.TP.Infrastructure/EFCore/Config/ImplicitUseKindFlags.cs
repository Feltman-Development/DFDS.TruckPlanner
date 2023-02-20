namespace FDEV.ISDDB.Infrastructure.EFCore.Config;

[Flags]
internal enum ImplicitUseKindFlags
{
    None = 0,
    Default = Access | Assign | InstantiatedWithFixedConstructorSignature,
    Access = 1,
    Assign = 2,
    InstantiatedWithFixedConstructorSignature = 4,
    InstantiatedNoFixedConstructorSignature = 8
}
