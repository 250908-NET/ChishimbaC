using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Services.Impl;

public class RoomService(IRoomRepository repo) : IRoomService
{
    public Task<List<Room>> GetAllAsync(CancellationToken ct = default) => repo.GetAllAsync(ct);
    public Task<Room?> GetAsync(int id, CancellationToken ct = default) => repo.GetAsync(id, ct);

    public async Task<Room> CreateAsync(string number, int capacity, decimal nightlyRate, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(number) || capacity <= 0 || nightlyRate < 0)
            throw new ArgumentException("Number, Capacity (>0), and NightlyRate (>=0) are required.");

        var normalized = number.Trim().ToUpperInvariant();
        var dup = await repo.GetByNumberAsync(normalized, ct);
        if (dup is not null) throw new InvalidOperationException("Room number must be unique.");

        var room = new Room { Number = normalized, Capacity = capacity, NightlyRate = nightlyRate };
        return await repo.AddAsync(room, ct);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default) => repo.DeleteAsync(id, ct);
}
