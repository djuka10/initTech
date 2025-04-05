using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class VehicleDto
    {
        public int VehicleId { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public string FuelType { get; set; }
    }
}
