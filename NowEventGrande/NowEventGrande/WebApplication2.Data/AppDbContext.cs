using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAddress> ClientAddress{ get; set; }
        public DbSet<Event> Events{ get; set; }
        public DbSet<EventAddress> EventAddress{ get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<Offer> Offer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //new DbInitializer(modelBuilder).Seed();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>()
                .HasData(
                    new Event
                    {
                        Id = 1,
                        Size = "Small",
                        Type = "Birthday",
                        Name = "BirthdayEvent",
                        Status = ""
                    });
            modelBuilder.Entity<Event>()
                .HasData(
                    new Event
                    {
                        Id = 2,
                        Size = "Large",
                        Type = "Festival",
                        Name = "FestivalEvent",
                        Status = ""

                    });
            modelBuilder.Entity<Guest>()
                .HasData(
                    new Guest
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john@gmail.com",
                        EventId = 1
                    });
        }
    }
}