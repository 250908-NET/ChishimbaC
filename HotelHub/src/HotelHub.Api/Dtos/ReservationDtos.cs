namespace HotelHub.Api.Dtos;

public record ReservationCreateDto(int GuestId, int RoomId, DateTime CheckIn, DateTime CheckOut);
public record ReservationReadDto(int Id, int GuestId, int RoomId, DateTime CheckIn, DateTime CheckOut, decimal TotalPrice);
