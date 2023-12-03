using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTicketReservation.Models;

namespace MovieTicketReservation.Areas.Identity.Data;

public class MovieTicketReservationContext : IdentityDbContext<IdentityUser>
{

    public DbSet<MovieShows> MovieShows { get; set; }
    public DbSet<Halls> Halls { get; set; }
    public DbSet<Movies> Movies { get; set; }
    public DbSet<Reservations> Reservations { get; set; }
    public DbSet<SeatReservations> SeatReservations { get; set; }
    

    public MovieTicketReservationContext(DbContextOptions<MovieTicketReservationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
