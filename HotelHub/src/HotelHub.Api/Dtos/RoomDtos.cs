namespace HotelHub.Api.Dtos;

public record RoomCreateDto(string Number, int Capacity, decimal NightlyRate);
public record RoomReadDto(int Id, string Number, int Capacity, decimal NightlyRate, string[] Amenities);
