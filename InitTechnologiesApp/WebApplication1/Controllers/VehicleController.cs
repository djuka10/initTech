using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/vehicles")]
    [Produces("application/json", "application/xml")]
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IMapper mapper;

        public VehicleController(IVehicleRepository vehicleRepository, IMapper mapper)
        {             
            this.vehicleRepository = vehicleRepository;
            this.mapper = mapper;
        }

        [HttpPost("create")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Vehicle> CreateVehicle([FromBody] VehicleDto vehicleDto)
        {
            try
            {
                Vehicle v = mapper.Map<Vehicle>(vehicleDto);
                vehicleRepository.CreateVehicle(v);
                vehicleRepository.SaveChanges();

                return Created("", v);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
            {
                return Conflict("Vehicle with the same Vehicle Name already exists.");
            }
            catch (DbUpdateException ex)
            {
                Exception? e = ex.InnerException;
                if (e != null)
                {
                    return BadRequest(e.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating a new vehicle. Please try again later.");
            }
        }

        [HttpPut("update")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VehicleDto> UpdateVehicle(VehicleDto vehicle)
        {
            try
            {
                var old = vehicleRepository.GetVehicleById(vehicle.VehicleId);
                if (old == null)
                {
                    return NotFound();
                }
                Vehicle vehicleEntity = mapper.Map<Vehicle>(vehicle);

                mapper.Map(vehicleEntity, old);

                vehicleRepository.SaveChanges();
                return Ok(mapper.Map<VehicleDto>(old));
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
            {
                return Conflict("Vehicle with the same Vehicle Name already exists.");
            }
            catch (DbUpdateException ex)
            {
                Exception? e = ex.InnerException;
                if (e != null)
                {
                    return BadRequest(e.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the vehicle. Please try again later.");
            }
        }

        [HttpDelete("delete/{vehicleId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteVehicle(int vehicleId)
        {
            try
            {
                var vehicle = vehicleRepository.GetVehicleById(vehicleId);

                if (vehicle == null)
                {
                    return NotFound();
                }

                vehicleRepository.DeleteVehicle(vehicleId);
                vehicleRepository.SaveChanges();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the vehicle. Please try again later.");
            }
        }

        [HttpGet("get/{vehicleId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VehicleDto> GetBrand(int vehicleId)
        {
            var vehicle = vehicleRepository.GetVehicleById(vehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<VehicleDto>(vehicle));
        }

        [HttpGet("vehicles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<VehicleDto>> GetBrands()
        {
            var vehicles = vehicleRepository.GetVehicles();

            if (vehicles == null || vehicles.Count == 0)
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<VehicleDto>>(vehicles));
        }
    }
}
