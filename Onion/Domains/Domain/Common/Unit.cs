namespace Domains.Common;

public sealed class Unit
{
    private Unit()
    {
    }

    public static Unit Value { get; } = new();
}