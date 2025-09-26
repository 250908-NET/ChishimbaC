using HotelHub.Api.Models;

namespace HotelHub.Api.Services.Interfaces;

public interface IReservationService
{
    Task<Reservation> CreateAsync(int guestId, int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default);
    Task<List<Reservation>> GetAllAsync(CancellationToken ct = default);
    Task<Reservation?> GetAsync(int id, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<bool> IsAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut, CancellationToken ct = default);
}
