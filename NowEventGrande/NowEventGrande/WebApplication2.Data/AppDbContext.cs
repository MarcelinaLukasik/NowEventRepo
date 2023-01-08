using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Event> Events{ get; set; }
        public DbSet<EventAddress> EventAddress{ get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<Offer> Offer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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