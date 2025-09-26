namespace HotelHub.Api.Models;

public class Reservation
{
    public int Id { get; set; }
    public int GuestId { get; set; }
    public Guest Guest { get; set; } = default!;

    public int RoomId { get; set; }
    public Room Room { get; set; } = default!;

    public DateTime CheckIn { get; set; }    // inclusive
    public DateTime CheckOut { get; set; }   // exclusive
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
