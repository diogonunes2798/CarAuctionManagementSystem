using CarAuction.Models;
using CarAuction.Models.Requests;
using CarAuction.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{
    [ApiController]
    [Route("api")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost("Vehicles")]
        public IActionResult CreateVehicle([FromBody] CreateVehicle newVehicle)
        {
            try
            {
                var entity = Mapper.RequestToEntity(newVehicle);
                if (entity == null)
                    return BadRequest("Invalid Vehicle data.");

                _vehicleService.CreateVehicle(entity);

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Vehicles/All")]
        public IActionResult GetAllVehicles()
        {
            try
            {
                var result = _vehicleService.GetAllVehicles();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Vehicles/Type/{vehicleType}")]
        public IActionResult GetVehiclesByType(string vehicleType)
        {
            try
            {
                if (!Enum.TryParse<VehicleType>(vehicleType, ignoreCase: true, out var parsedType))
                    return BadRequest($"Invalid vehicle type: {vehicleType}");

                var result = _vehicleService.GetVehiclesByType(parsedType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Vehicles/Model/{model}")]
        public IActionResult GetVehiclesByModel(string model)
        {
            try
            {
                var result = _vehicleService.GetVehiclesByModel(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Vehicles/Manufacturer/{manufacturer}")]
        public IActionResult GetVehiclesByManufacturer(string manufacturer)
        {
            try
            {
                var result = _vehicleService.GetVehiclesByManufacturer(manufacturer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Vehicles/Year/{year}")]
        public IActionResult GetVehiclesByYear(int year)
        {
            try
            {
                var result = _vehicleService.GetVehiclesByYear(year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Vehicles/{id}")]
        public IActionResult GetVehiclesById(Guid id)
        {
            try
            {
                var result = _vehicleService.GetVehicleById(id);
                if (result is null)
                    return NotFound($"Vehicle with ID '{id}' not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
