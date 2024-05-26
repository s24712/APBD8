using EntityFrameworkProj.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProj.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Trip> Trips { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientTrip> ClientTrips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientTrip>()
            .HasKey(ct => new { ct.IdClient, ct.IdTrip });

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Client)
            .WithMany(c => c.ClientTrips)
            .HasForeignKey(ct => ct.IdClient);

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Trip)
            .WithMany(t => t.ClientTrips)
            .HasForeignKey(ct => ct.IdTrip);

        modelBuilder.Entity<Trip>().ToTable("Trips", schema: "trip");
        modelBuilder.Entity<Client>().ToTable("Clients", schema: "trip");
        modelBuilder.Entity<ClientTrip>().ToTable("Client_Trips", schema: "trip");
    }
}