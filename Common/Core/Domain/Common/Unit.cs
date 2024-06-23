namespace ArchitectureConcepts.Common.Core.Domain.Common;

public sealed class Unit
{
    private Unit()
    {
    }

    public static Unit Value { get; } = new();
}