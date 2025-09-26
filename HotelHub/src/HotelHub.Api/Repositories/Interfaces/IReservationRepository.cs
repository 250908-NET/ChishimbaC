using HotelHub.Api.Models;

namespace HotelHub.Api.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<Reservation?> GetAsync(int id, CancellationToken ct = default);
    Task<List<Reservation>> GetAllAsync(CancellationToken ct = default);
    Task<bool> HasConflictAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default);
    Task<Reservation> AddAsync(Reservation reservation, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
