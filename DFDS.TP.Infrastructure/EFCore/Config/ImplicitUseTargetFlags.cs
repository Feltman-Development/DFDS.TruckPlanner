namespace FDEV.ISDDB.Infrastructure.EFCore.Config;

[Flags]
internal enum ImplicitUseTargetFlags
{
    Default = Itself,
    Itself = 1,
    Members = 2,
    WithMembers = Itself | Members
}
