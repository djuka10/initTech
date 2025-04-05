using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Database
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("InitTechnologiesDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>()
                .HasData(new
                {
                    VehicleId = 1,
                    Name = "Skoda Kamiq",
                    Year = 2022,
                    FuelType = "Petrol"
                });

            builder.Entity<Vehicle>()
                .HasData(new
                {
                    VehicleId = 2,
                    Name = "Skoda Superb",
                    Year = 2022,
                    FuelType = "Petrol"
                });

            builder.Entity<Vehicle>()
                .HasData(new
                {
                    VehicleId = 3,
                    Name = "Opel Vectra",
                    Year = 2002,
                    FuelType = "Diesel"
                });
        }
    }
}
