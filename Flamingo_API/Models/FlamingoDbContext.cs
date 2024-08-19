using Microsoft.EntityFrameworkCore;
using Flamingo_API.Models;

public class FlamingoDbContext : DbContext
{
    public FlamingoDbContext(DbContextOptions<FlamingoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    // Flight to Booking (one-to-many)
    //    modelBuilder.Entity<Flight>()
    //        .HasMany(f => f.Bookings)
    //        .WithOne(b => b.Flight)
    //        .HasForeignKey(b => b.FlightId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // User to Booking (one-to-many)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.Bookings)
    //        .WithOne(b => b.User)
    //        .HasForeignKey(b => b.UserId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Booking to Ticket (one-to-many)
    //    modelBuilder.Entity<Booking>()
    //        .HasMany(b => b.Tickets)
    //        .WithOne(t => t.Booking)
    //        .HasForeignKey(t => t.BookingId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Booking to Payment (one-to-one)
    //    modelBuilder.Entity<Booking>()
    //        .HasOne(b => b.Payment)
    //        .WithOne(p => p.Booking)
    //        .HasForeignKey<Payment>(p => p.BookingId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Unique constraint on PNR in Booking
    //    modelBuilder.Entity<Booking>()
    //        .HasIndex(b => b.PNR)
    //        .IsUnique();

    //    // Configure primary keys if composite or not convention-based
    //    // For example, if Booking had composite keys, configure them here
    //    // modelBuilder.Entity<Booking>()
    //    //     .HasKey(b => new { b.FlightId, b.UserId });

    //    // Additional configuration if needed
    //}
}
