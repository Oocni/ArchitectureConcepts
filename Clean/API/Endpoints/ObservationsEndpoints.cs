using ArchitectureConcepts.Clean.Core.Application.Observations.Add;
using ArchitectureConcepts.Clean.Core.Application.Observations.Delete;
using ArchitectureConcepts.Clean.Core.Application.Observations.Get;
using ArchitectureConcepts.Clean.Core.Application.Observations.GetAll;
using ArchitectureConcepts.Clean.Core.Application.Observations.Update;
using ArchitectureConcepts.Common.External.API.Endpoints;
using MediatR;

namespace ArchitectureConcepts.Clean.External.API.Endpoints;

public static class ObservationsEndpoints
{
    public static void MapObservationsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/observations/{id:int}", async (int id, IMediator mediator) =>
        {
            var query = new GetObservationQuery(id);
            var observationResponse = await mediator.Send(query);
            return observationResponse is not null
                ? Results.Ok(observationResponse)
                : Results.NotFound();
        });

        endpoints.MapGet("api/observations", async (int? id, int? limit, IMediator mediator) =>
        {
            var query = new GetAllObservationsQuery(id, limit);
            var observations = await mediator.Send(query);
            return Results.Ok(observations);
        });

        endpoints.MapPost("api/observations",
            async (CreateObservationRequest request, IMediator mediator) =>
            {
                var command = new AddObservationCommand(request.Name, request.CreatedBy, request.Description);
                var result = await mediator.Send(command);
                return result.IsSuccessful
                    ? Results.Created("api/observations/" + result.Value, "api/observations/" + result.Value)
                    : Results.BadRequest(result.Error.Message);
            });

        endpoints.MapPut("api/observations/{id:int}",
            async (int id, UpdateObservationRequest request, IMediator mediator) =>
            {
                var command = new UpdateObservationCommand(id, request.Name, request.Description);
                var result = await mediator.Send(command);
                return result.IsSuccessful
                    ? Results.NoContent()
                    : Results.BadRequest(result.Error.Message);
            });

        endpoints.MapDelete("api/observations/{id:int}",
            async (int id, string deletedBy, IMediator mediator) =>
            {
                var command = new DeleteObservationCommand(id, deletedBy);
                var result = await mediator.Send(command);
                return result.IsSuccessful
                    ? Results.NoContent()
                    : Results.BadRequest(result.Error.Message);
            });
    }
}