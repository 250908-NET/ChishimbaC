using HotelHub.Api.Dtos;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Endpoints;

public static class ReservationsEndpoints
{
    public static IEndpointRouteBuilder MapReservationsEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/reservations").WithTags("Reservations");

        g.MapGet("/", async (IReservationService svc, CancellationToken ct) =>
        {
            var items = await svc.GetAllAsync(ct);
            return items.Select(x => new ReservationReadDto(x.Id, x.GuestId, x.RoomId, x.CheckIn, x.CheckOut, x.TotalPrice));
        });

        g.MapGet("/{id:int}", async (int id, IReservationService svc, CancellationToken ct) =>
        {
            var x = await svc.GetAsync(id, ct);
            return x is null ? Results.NotFound()
                : Results.Ok(new ReservationReadDto(x.Id, x.GuestId, x.RoomId, x.CheckIn, x.CheckOut, x.TotalPrice));
        });

        g.MapGet("/availability", async (int roomId, DateTime checkIn, DateTime checkOut, IReservationService svc, CancellationToken ct) =>
        {
            try
            {
                var available = await svc.IsAvailableAsync(roomId, checkIn, checkOut, ct);
                return Results.Ok(new { roomId, checkIn = checkIn.Date, checkOut = checkOut.Date, available });
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
        });

        g.MapPost("/", async (ReservationCreateDto dto, IReservationService svc, CancellationToken ct) =>
        {
            try
            {
                var created = await svc.CreateAsync(dto.GuestId, dto.RoomId, dto.CheckIn, dto.CheckOut, ct);
                return Results.Created($"/reservations/{created.Id}",
                    new ReservationReadDto(created.Id, created.GuestId, created.RoomId, created.CheckIn, created.CheckOut, created.TotalPrice));
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
            catch (InvalidOperationException ex) { return Results.Conflict(ex.Message); }
        });

        g.MapDelete("/{id:int}", async (int id, IReservationService svc, CancellationToken ct) =>
        {
            var ok = await svc.DeleteAsync(id, ct);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}
