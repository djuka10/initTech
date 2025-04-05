using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IVehicleRepository
    {
        bool SaveChanges();

        List<Vehicle> GetVehicles();

        Vehicle GetVehicleById(int vehicleId);

        void DeleteVehicle(int vehicleId);

        Vehicle CreateVehicle(Vehicle vehicle);

        //ef core prati entitet iz baze i kad promenio i odradimo save changes izmene se zadrze
    }
}
