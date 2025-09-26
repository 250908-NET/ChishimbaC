using System;
using System.Threading.Tasks;
using HotelHub.Api.Data;
using HotelHub.Api.Models;
using HotelHub.Api.Repositories.Ef;
using HotelHub.Api.Services.Impl;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ReservationServiceTests
{
    private static HotelDbContext NewInMemoryDb()
    {
        var conn = new SqliteConnection("DataSource=:memory:");
        conn.Open();
        var options = new DbContextOptionsBuilder<HotelHub.Api.Data.HotelDbContext>()
            .UseSqlite(conn).Options;
        var db = new HotelDbContext(options);
        db.Database.EnsureCreated();
        return db;
    }

    [Fact]
    public async Task Rejects_Overlapping_Reservations()
    {
        using var db = NewInMemoryDb();

        var guestRepo = new GuestRepository(db);
        var roomRepo = new RoomRepository(db);
        var resRepo = new ReservationRepository(db);

        var guest = await guestRepo.AddAsync(new Guest { FirstName = "Test", LastName = "Guest" });
        var room = await roomRepo.AddAsync(new Room { Number = "101", Capacity = 2, NightlyRate = 100m });

        var service = new ReservationService(resRepo, roomRepo, guestRepo);

        var r1 = await service.CreateAsync(guest.Id, room.Id, new DateTime(2025,10,10), new DateTime(2025,10,13));
        Assert.True(r1.TotalPrice > 0);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await service.CreateAsync(guest.Id, room.Id, new DateTime(2025,10,12), new DateTime(2025,10,15));
        });
    }

    [Fact]
    public async Task Allows_BackToBack_Reservations()
    {
        using var db = NewInMemoryDb();

        var guestRepo = new GuestRepository(db);
        var roomRepo = new RoomRepository(db);
        var resRepo = new ReservationRepository(db);

        var g1 = await guestRepo.AddAsync(new Guest { FirstName = "A", LastName = "One" });
        var g2 = await guestRepo.AddAsync(new Guest { FirstName = "B", LastName = "Two" });
        var room = await roomRepo.AddAsync(new Room { Number = "102", Capacity = 2, NightlyRate = 100m });

        var service = new ReservationService(resRepo, roomRepo, guestRepo);

        var r1 = await service.CreateAsync(g1.Id, room.Id, new DateTime(2025,11,01), new DateTime(2025,11,05));
        var r2 = await service.CreateAsync(g2.Id, room.Id, new DateTime(2025,11,05), new DateTime(2025,11,07));

        Assert.NotEqual(r1.Id, r2.Id);
    }
}
