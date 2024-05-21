using ArchitectureConcepts.Clean.Core.Domain.Common;
using ArchitectureConcepts.Clean.Core.Persistence.Database;
using DotNext;

namespace ArchitectureConcepts.Clean.Core.Persistence.UnitOfWork;

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