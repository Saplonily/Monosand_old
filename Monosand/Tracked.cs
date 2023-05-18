namespace Monosand;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class Tracked : Attribute
{
    // 子类是否也被track
    public bool Inherit = false;
}