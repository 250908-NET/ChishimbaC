using HotelHub.Api.Dtos;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Endpoints;

public static class GuestsEndpoints
{
    public static IEndpointRouteBuilder MapGuestsEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/guests").WithTags("Guests");

        g.MapGet("/", async (IGuestService svc, CancellationToken ct) =>
        {
            var items = await svc.GetAllAsync(ct);
            return items.Select(x => new GuestReadDto(x.Id, x.FirstName, x.LastName, x.Email, x.CreatedAt));
        });

        g.MapGet("/{id:int}", async (int id, IGuestService svc, CancellationToken ct) =>
        {
            var x = await svc.GetAsync(id, ct);
            return x is null ? Results.NotFound() :
                Results.Ok(new GuestReadDto(x.Id, x.FirstName, x.LastName, x.Email, x.CreatedAt));
        });

        g.MapPost("/", async (GuestCreateDto dto, IGuestService svc, CancellationToken ct) =>
        {
            try
            {
                var created = await svc.CreateAsync(dto.FirstName, dto.LastName, dto.Email, ct);
                return Results.Created($"/guests/{created.Id}",
                    new GuestReadDto(created.Id, created.FirstName, created.LastName, created.Email, created.CreatedAt));
            }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
        });

        g.MapDelete("/{id:int}", async (int id, IGuestService svc, CancellationToken ct) =>
        {
            var ok = await svc.DeleteAsync(id, ct);
            return ok ? Results.NoContent() : Results.NotFound();
        });

        return app;
    }
}
