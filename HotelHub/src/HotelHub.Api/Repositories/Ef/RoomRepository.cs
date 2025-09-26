using HotelHub.Api.Data;
using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Api.Repositories.Ef;

public class RoomRepository(HotelDbContext db) : IRoomRepository
{
    public async Task<Room?> GetAsync(int id, CancellationToken ct = default) =>
        await db.Rooms.Include(r => r.RoomAmenities).ThenInclude(ra => ra.Amenity)
                      .FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<Room?> GetByNumberAsync(string number, CancellationToken ct = default) =>
        await db.Rooms.FirstOrDefaultAsync(r => r.Number == number, ct);

    public async Task<List<Room>> GetAllAsync(CancellationToken ct = default) =>
        await db.Rooms.Include(r => r.RoomAmenities).ThenInclude(ra => ra.Amenity)
                      .AsNoTracking().ToListAsync(ct);

    public async Task<Room> AddAsync(Room room, CancellationToken ct = default)
    {
        db.Rooms.Add(room);
        await db.SaveChangesAsync(ct);
        return room;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Rooms.FindAsync([id], ct);
        if (entity is null) return false;
        db.Rooms.Remove(entity);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
