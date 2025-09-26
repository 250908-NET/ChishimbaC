using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Services.Impl;

public class GuestService(IGuestRepository repo) : IGuestService
{
    public Task<List<Guest>> GetAllAsync(CancellationToken ct = default) => repo.GetAllAsync(ct);
    public Task<Guest?> GetAsync(int id, CancellationToken ct = default) => repo.GetAsync(id, ct);

    public async Task<Guest> CreateAsync(string firstName, string lastName, string? email, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("FirstName and LastName are required.");
        var guest = new Guest { FirstName = firstName.Trim(), LastName = lastName.Trim(), Email = email?.Trim() };
        return await repo.AddAsync(guest, ct);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default) => repo.DeleteAsync(id, ct);
}
