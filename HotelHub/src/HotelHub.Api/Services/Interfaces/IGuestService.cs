using HotelHub.Api.Models;

namespace HotelHub.Api.Services.Interfaces;

public interface IGuestService
{
    Task<Guest> CreateAsync(string firstName, string lastName, string? email, CancellationToken ct = default);
    Task<List<Guest>> GetAllAsync(CancellationToken ct = default);
    Task<Guest?> GetAsync(int id, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
