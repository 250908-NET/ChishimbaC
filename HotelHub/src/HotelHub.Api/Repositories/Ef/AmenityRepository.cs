using HotelHub.Api.Data;
using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Api.Repositories.Ef;

public class AmenityRepository(HotelDbContext db) : IAmenityRepository
{
    public async Task<List<Amenity>> GetAllAsync(CancellationToken ct = default) =>
        await db.Amenities.AsNoTracking().ToListAsync(ct);

    public async Task<Amenity?> GetAsync(int id, CancellationToken ct = default) =>
        await db.Amenities.FindAsync([id], ct);

    public async Task<Amenity> AddAsync(Amenity amenity, CancellationToken ct = default)
    {
        db.Amenities.Add(amenity);
        await db.SaveChangesAsync(ct);
        return amenity;
    }

    public async Task<bool> AddToRoomAsync(int roomId, int amenityId, CancellationToken ct = default)
    {
        var exists = await db.RoomAmenities.AnyAsync(x => x.RoomId == roomId && x.AmenityId == amenityId, ct);
        if (exists) return false;
        var roomExists = await db.Rooms.AnyAsync(r => r.Id == roomId, ct);
        var amenityExists = await db.Amenities.AnyAsync(a => a.Id == amenityId, ct);
        if (!roomExists || !amenityExists) return false;

        db.RoomAmenities.Add(new RoomAmenity { RoomId = roomId, AmenityId = amenityId });
        await db.SaveChangesAsync(ct);
        return true;
    }
}
