using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Services.Impl;

public class AmenityService(IAmenityRepository repo) : IAmenityService
{
    public Task<List<Amenity>> GetAllAsync(CancellationToken ct = default) => repo.GetAllAsync(ct);

    public async Task<Amenity> CreateAsync(string name, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.");
        return await repo.AddAsync(new Amenity { Name = name.Trim() }, ct);
    }

    public Task<bool> AddToRoomAsync(int roomId, int amenityId, CancellationToken ct = default) =>
        repo.AddToRoomAsync(roomId, amenityId, ct);
}
