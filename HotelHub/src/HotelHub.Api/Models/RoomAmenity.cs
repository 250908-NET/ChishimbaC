namespace HotelHub.Api.Models;

public class RoomAmenity
{
    public int RoomId { get; set; }
    public Room Room { get; set; } = default!;
    public int AmenityId { get; set; }
    public Amenity Amenity { get; set; } = default!;
}

