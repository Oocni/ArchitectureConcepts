using DotNext;

namespace ArchitectureConcepts.Common.Core.Domain.Common;

public interface IUnitOfWork
{
    Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken = default);
}