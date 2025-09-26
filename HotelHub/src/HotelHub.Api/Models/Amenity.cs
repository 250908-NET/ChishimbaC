namespace HotelHub.Api.Models;

public class Amenity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<RoomAmenity> RoomAmenities { get; set; } = new List<RoomAmenity>();
}
