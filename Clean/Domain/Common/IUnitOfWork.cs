using DotNext;

namespace ArchitectureConcepts.Clean.Core.Domain.Common;

public interface IUnitOfWork
{
    /// <summary>
    /// Save changes to the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns the number of changes made in database. </returns>
    Task<Result<int>> SaveChangesAsync(CancellationToken cancellationToken = default);
}