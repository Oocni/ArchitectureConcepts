using ArchitectureConcepts.Clean.Core.Application.Observations;
using ArchitectureConcepts.Clean.Core.Application.Observations.GetAll;
using ArchitectureConcepts.Common.External.Persistance.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureConcepts.Clean.External.Persistence.Queries.Observations.GetAll;

public class GetAllObservationsHandler(ApplicationDbContext context)
    : IRequestHandler<GetAllObservationsQuery, IEnumerable<ObservationResponse>>
{
    public async Task<IEnumerable<ObservationResponse>> Handle(GetAllObservationsQuery query, CancellationToken cancellationToken)
    {
        var observationQuery = context
            .Observations
            .AsQueryable();

        if (query.Id is not null)
        {
            observationQuery = observationQuery.Where(o => o.Id >= query.Id);
        }

        var limit = query.Limit.HasValue
            ? query.Limit > 100 
                ? 100
                : query.Limit.Value
            : 10;

        return await observationQuery
            .AsNoTracking()
            .OrderBy(o => o.Id)
            .Take(limit)
            .Select(o => (ObservationResponse)o!)
            .ToListAsync(cancellationToken);
    }
}