using HotelHub.Api.Data;
using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Api.Repositories.Ef;

public class ReservationRepository(HotelDbContext db) : IReservationRepository
{
    public async Task<Reservation?> GetAsync(int id, CancellationToken ct = default) =>
        await db.Reservations.FindAsync([id], ct);

    public async Task<List<Reservation>> GetAllAsync(CancellationToken ct = default) =>
        await db.Reservations.AsNoTracking().ToListAsync(ct);

    public async Task<bool> HasConflictAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default)
    {
        return await db.Reservations.AnyAsync(r =>
            r.RoomId == roomId &&
            checkIn < r.CheckOut &&
            checkOut > r.CheckIn, ct);
    }

    public async Task<Reservation> AddAsync(Reservation reservation, CancellationToken ct = default)
    {
        db.Reservations.Add(reservation);
        await db.SaveChangesAsync(ct);
        return reservation;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await db.Reservations.FindAsync([id], ct);
        if (entity is null) return false;
        db.Reservations.Remove(entity);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
