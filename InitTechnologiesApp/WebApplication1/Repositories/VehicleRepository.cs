using AutoMapper;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext context;

        public VehicleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteVehicle(int vehicleId)
        {
            var vehicle = GetVehicleById(vehicleId);
            context.Remove(vehicle);
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            return context.Vehicles.FirstOrDefault(a => a.VehicleId == vehicleId);
        }

        public List<Vehicle> GetVehicles()
        {
            return context.Vehicles.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Vehicle CreateVehicle(Vehicle vehicle)
        {
            var createdEntity = context.Add(vehicle);
            return createdEntity.Entity;
        }

    }
}
