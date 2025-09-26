namespace HotelHub.Api.Dtos;

public record GuestCreateDto(string FirstName, string LastName, string? Email);
public record GuestReadDto(int Id, string FirstName, string LastName, string? Email, DateTime CreatedAt);
