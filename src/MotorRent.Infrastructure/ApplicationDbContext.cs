using Microsoft.EntityFrameworkCore;
using MotorRent.Domain.Entities;

namespace MotorRent.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<DeliveryRider> Riders { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
