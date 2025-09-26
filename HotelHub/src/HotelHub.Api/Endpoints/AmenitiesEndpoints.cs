using HotelHub.Api.Dtos;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Endpoints;

public static class AmenitiesEndpoints
{
    public static IEndpointRouteBuilder MapAmenitiesEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/amenities").WithTags("Amenities");

        g.MapGet("/", async (IAmenityService svc, CancellationToken ct) =>
            (await svc.GetAllAsync(ct)).Select(a => new AmenityReadDto(a.Id, a.Name)));

        g.MapPost("/", async (AmenityCreateDto dto, IAmenityService svc, CancellationToken ct) =>
        {
            try
            {
                var created = await svc.CreateAsync(dto.Name, ct);
                return Results.Created($"/amenities/{created.Id}", new AmenityReadDto(created.Id, created.Name));
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
        });

        g.MapPost("/rooms/{roomId:int}/{amenityId:int}", async (int roomId, int amenityId, IAmenityService svc, CancellationToken ct) =>
        {
            var ok = await svc.AddToRoomAsync(roomId, amenityId, ct);
            return ok ? Results.Ok() : Results.BadRequest("Invalid room/amenity or already assigned.");
        });

        return app;
    }
}
