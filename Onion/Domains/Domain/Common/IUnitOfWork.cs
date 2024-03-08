using DotNext;

namespace Domains.Common;

public interface IUnitOfWork
{
    Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken = default);
}