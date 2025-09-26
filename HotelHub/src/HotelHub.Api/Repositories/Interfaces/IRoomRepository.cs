using HotelHub.Api.Models;

namespace HotelHub.Api.Repositories.Interfaces;

public interface IRoomRepository
{
    Task<Room?> GetAsync(int id, CancellationToken ct = default);
    Task<Room?> GetByNumberAsync(string number, CancellationToken ct = default);
    Task<List<Room>> GetAllAsync(CancellationToken ct = default);
    Task<Room> AddAsync(Room room, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
