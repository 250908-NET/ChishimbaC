using HotelHub.Api.Models;

namespace HotelHub.Api.Services.Interfaces;

public interface IAmenityService
{
    Task<Amenity> CreateAsync(string name, CancellationToken ct = default);
    Task<List<Amenity>> GetAllAsync(CancellationToken ct = default);
    Task<bool> AddToRoomAsync(int roomId, int amenityId, CancellationToken ct = default);
}
