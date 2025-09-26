using HotelHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Api.Data;

public static class Seed
{
    public static async Task ApplyAsync(HotelDbContext db)
    {
        await db.Database.MigrateAsync();

        if (!await db.Rooms.AnyAsync())
        {
            db.Rooms.AddRange(
                new Room { Number = "101", Capacity = 2, NightlyRate = 129.99m },
                new Room { Number = "102", Capacity = 4, NightlyRate = 179.99m }
            );
        }

        if (!await db.Amenities.AnyAsync())
        {
            db.Amenities.AddRange(
                new Amenity { Name = "Wi-Fi" },
                new Amenity { Name = "Breakfast" },
                new Amenity { Name = "Pool" }
            );
        }

        if (!await db.Guests.AnyAsync())
        {
            db.Guests.Add(new Guest { FirstName = "Marie", LastName = "Curie", Email = "marie@example.com" });
        }

        await db.SaveChangesAsync();

        var wifi = await db.Amenities.FirstAsync(a => a.Name == "Wi-Fi");
        var pool = await db.Amenities.FirstAsync(a => a.Name == "Pool");
        var r101Id = await db.Rooms.Where(r => r.Number == "101").Select(r => r.Id).FirstAsync();
        var r102Id = await db.Rooms.Where(r => r.Number == "102").Select(r => r.Id).FirstAsync();

        if (!await db.RoomAmenities.AnyAsync())
        {
            db.RoomAmenities.AddRange(
                new RoomAmenity { RoomId = r101Id, AmenityId = wifi.Id },
                new RoomAmenity { RoomId = r102Id, AmenityId = wifi.Id },
                new RoomAmenity { RoomId = r102Id, AmenityId = pool.Id }
            );
        }

        await db.SaveChangesAsync();
    }
}
