using ArchitectureConcepts.Common.Core.Domain.Common;
using ArchitectureConcepts.Common.External.Persistance.Database;
using DotNext;

namespace ArchitectureConcepts.Common.External.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return Result.FromException<int>(e);
        }
    }
}