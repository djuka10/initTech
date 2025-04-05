using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string FuelType { get; set; }
    }
}
