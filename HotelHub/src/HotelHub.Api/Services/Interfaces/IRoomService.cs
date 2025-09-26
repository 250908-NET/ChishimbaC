using HotelHub.Api.Models;

namespace HotelHub.Api.Services.Interfaces;

public interface IRoomService
{
    Task<Room> CreateAsync(string number, int capacity, decimal nightlyRate, CancellationToken ct = default);
    Task<List<Room>> GetAllAsync(CancellationToken ct = default);
    Task<Room?> GetAsync(int id, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
