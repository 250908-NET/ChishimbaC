namespace HotelHub.Api.Models;

public class Room
{
    public int Id { get; set; }
    public required string Number { get; set; }
    public int Capacity { get; set; } = 2;
    public decimal NightlyRate { get; set; }

    public ICollection<RoomAmenity> RoomAmenities { get; set; } = new List<RoomAmenity>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
