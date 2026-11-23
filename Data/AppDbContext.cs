using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_pravotorova.Models;

namespace wt_lab12_12auth_validation_pravotorova.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    // НОВАЯ ТАБЛИЦА
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // (Оставь здесь свой код HasData и связей, если он был)
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Number = "101", Category = "Стандарт", Capacity = 2, PricePerNight = 3500, IsAvailable = true, Description = "Уютный номер", ImageUrl = "" },
            new Room { Id = 2, Number = "102", Category = "Люкс", Capacity = 2, PricePerNight = 9000, IsAvailable = true, Description = "С джакузи", ImageUrl = "" }
        );
    }
}