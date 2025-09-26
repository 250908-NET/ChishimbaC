using HotelHub.Api.Dtos;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Endpoints;

public static class RoomsEndpoints
{
    public static IEndpointRouteBuilder MapRoomsEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/rooms").WithTags("Rooms");

        g.MapGet("/", async (IRoomService rooms, CancellationToken ct) =>
        {
            var list = await rooms.GetAllAsync(ct);
            return list.Select(r => new RoomReadDto(
                r.Id, r.Number, r.Capacity, r.NightlyRate,
                r.RoomAmenities.Select(ra => ra.Amenity.Name).ToArray()
            ));
        });

        g.MapGet("/{id:int}", async (int id, IRoomService rooms, CancellationToken ct) =>
        {
            var r = await rooms.GetAsync(id, ct);
            if (r is null) return Results.NotFound();
            return Results.Ok(new RoomReadDto(
                r.Id, r.Number, r.Capacity, r.NightlyRate,
                r.RoomAmenities.Select(ra => ra.Amenity.Name).ToArray()
            ));
        });

        g.MapPost("/", async (RoomCreateDto dto, IRoomService rooms, CancellationToken ct) =>
        {
            try
            {
                var created = await rooms.CreateAsync(dto.Number, dto.Capacity, dto.NightlyRate, ct);
                return Results.Created($"/rooms/{created.Id}",
                    new RoomReadDto(created.Id, created.Number, created.Capacity, created.NightlyRate, Array.Empty<string>()));
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Results.Conflict(ex.Message); }
        });

        g.MapDelete("/{id:int}", async (int id, IRoomService rooms, CancellationToken ct) =>
        {
            var ok = await rooms.DeleteAsync(id, ct);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}
