using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Interfaces;
using HotelHub.Api.Services.Interfaces;

namespace HotelHub.Api.Services.Impl;

public class ReservationService(IReservationRepository reservations, IRoomRepository rooms, IGuestRepository guests)
    : IReservationService
{
    public Task<List<Reservation>> GetAllAsync(CancellationToken ct = default) => reservations.GetAllAsync(ct);
    public Task<Reservation?> GetAsync(int id, CancellationToken ct = default) => reservations.GetAsync(id, ct);
    public Task<bool> DeleteAsync(int id, CancellationToken ct = default) => reservations.DeleteAsync(id, ct);

    public async Task<bool> IsAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default)
    {
        if (checkOut <= checkIn) throw new ArgumentException("CheckOut must be after CheckIn.");
        return !await reservations.HasConflictAsync(roomId, checkIn.Date, checkOut.Date, ct);
    }

    public async Task<Reservation> CreateAsync(int guestId, int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default)
    {
        if (checkOut <= checkIn) throw new ArgumentException("CheckOut must be after CheckIn.");

        var room = await rooms.GetAsync(roomId, ct) ?? throw new ArgumentException("Invalid room.");
        _ = await guests.GetAsync(guestId, ct) ?? throw new ArgumentException("Invalid guest.");

        var conflict = await reservations.HasConflictAsync(roomId, checkIn.Date, checkOut.Date, ct);
        if (conflict) throw new InvalidOperationException("Room is already reserved in the requested dates.");

        var nights = Math.Max(1, (checkOut.Date - checkIn.Date).Days);
        var total = room.NightlyRate * nights;

        var entity = new Reservation
        {
            GuestId = guestId,
            RoomId = roomId,
            CheckIn = checkIn.Date,
            CheckOut = checkOut.Date,
            TotalPrice = total
        };
        return await reservations.AddAsync(entity, ct);
    }
}
