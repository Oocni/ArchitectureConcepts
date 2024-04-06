using DotNext;

namespace Domain.Common;

public interface IUnitOfWork
{
    Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken = default);
}