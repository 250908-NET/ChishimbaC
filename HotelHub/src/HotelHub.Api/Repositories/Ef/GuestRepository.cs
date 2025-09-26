using HotelHub.Api.Data;
using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Api.Repositories.Ef;

public class GuestRepository(HotelDbContext db) : IGuestRepository
{
    public async Task<Guest?> GetAsync(int id, CancellationToken ct = default) =>
        await db.Guests.FindAsync([id], ct);

    public async Task<List<Guest>> GetAllAsync(CancellationToken ct = default) =>
        await db.Guests.AsNoTracking().ToListAsync(ct);

    public async Task<Guest> AddAsync(Guest guest, CancellationToken ct = default)
    {
        db.Guests.Add(guest);
        await db.SaveChangesAsync(ct);
        return guest;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Guests.FindAsync([id], ct);
        if (entity is null) return false;
        db.Guests.Remove(entity);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
