using Application.Observations;

namespace API.EndPoints;

public static class ObservationsEndpoints
{
    public static void MapObservationsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("api/observations/{id:int}", async (int id, ObservationsService observationService) =>
        {
            var observationResponse = await observationService.GetAsync(id);
            return observationResponse is not null
                ? Results.Ok(observationResponse)
                : Results.NotFound();
        });

        endpoints.MapGet("api/observations", async (int? id, int? limit, ObservationsService observationService) =>
        {
            var observations = await observationService.GetAllAsync(id, limit ?? 10);
            return Results.Ok(observations);
        });

        endpoints.MapPost("api/observations",
            async (CreateObservationRequest request, ObservationsService observationService) =>
            {
                var parameters = new CreateObservationParameters(request.Name, request.CreatedBy, request.Description);
                var result = await observationService.AddAsync(parameters);
                return result.IsSuccessful
                    ? Results.Created("api/observations/" + result.Value, "api/observations/" + result.Value)
                    : Results.BadRequest(result.Error.Message);
            });

        endpoints.MapPut("api/observations/{id:int}",
            async (int id, UpdateObservationRequest request, ObservationsService observationService) =>
            {
                var result = await observationService.UpdateAsync(id, request.Name, request.Description);
                return result.IsSuccessful
                    ? Results.NoContent()
                    : Results.BadRequest(result.Error.Message);
            });

        endpoints.MapDelete("api/observations/{id:int}",
            async (int id, string deletedBy, ObservationsService observationService) =>
            {
                var result = await observationService.DeleteAsync(id, deletedBy);
                return result.IsSuccessful
                    ? Results.NoContent()
                    : Results.BadRequest(result.Error.Message);
            });
    }
}