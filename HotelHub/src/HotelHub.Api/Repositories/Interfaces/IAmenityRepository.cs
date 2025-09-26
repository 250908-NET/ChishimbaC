using HotelHub.Api.Models;

namespace HotelHub.Api.Repositories.Interfaces;

public interface IAmenityRepository
{
    Task<List<Amenity>> GetAllAsync(CancellationToken ct = default);
    Task<Amenity?> GetAsync(int id, CancellationToken ct = default);
    Task<Amenity> AddAsync(Amenity amenity, CancellationToken ct = default);
    Task<bool> AddToRoomAsync(int roomId, int amenityId, CancellationToken ct = default);
}
