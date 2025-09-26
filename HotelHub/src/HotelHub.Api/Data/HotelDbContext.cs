using HotelHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelHub.Api.Data;

public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Amenity> Amenities => Set<Amenity>();
    public DbSet<RoomAmenity> RoomAmenities => Set<RoomAmenity>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Guest>(e =>
        {
            e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasMaxLength(200);
        });

        model.Entity<Room>(e =>
        {
            e.HasIndex(x => x.Number).IsUnique();
            e.Property(x => x.Number).HasMaxLength(10).IsRequired();
            e.Property(x => x.NightlyRate).HasPrecision(18, 2);
        });

        model.Entity<Amenity>(e =>
        {
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
        });

        model.Entity<RoomAmenity>(e =>
        {
            e.HasKey(x => new { x.RoomId, x.AmenityId });
            e.HasOne(x => x.Room).WithMany(r => r.RoomAmenities).HasForeignKey(x => x.RoomId);
            e.HasOne(x => x.Amenity).WithMany(a => a.RoomAmenities).HasForeignKey(x => x.AmenityId);
        });

        model.Entity<Reservation>(e =>
        {
            e.Property(x => x.TotalPrice).HasPrecision(18, 2);
            e.HasOne(x => x.Guest).WithMany(g => g.Reservations).HasForeignKey(x => x.GuestId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Room).WithMany(r => r.Reservations).HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(x => new { x.RoomId, x.CheckIn, x.CheckOut });
        });
    }
}
