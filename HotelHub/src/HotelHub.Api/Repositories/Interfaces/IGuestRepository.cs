using HotelHub.Api.Models;

namespace HotelHub.Api.Repositories.Interfaces;

public interface IGuestRepository
{
    Task<Guest?> GetAsync(int id, CancellationToken ct = default);
    Task<List<Guest>> GetAllAsync(CancellationToken ct = default);
    Task<Guest> AddAsync(Guest guest, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
