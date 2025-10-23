using CarAuction.Models;
using CarAuction.Models.Requests;
using CarAuction.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly IVehicleService _vehicleService;

        public AuctionController(IAuctionService auctionService, IVehicleService vehicleService)
        {
            _auctionService = auctionService;
            _vehicleService = vehicleService;
        }

        [HttpPost("Auctions")]
        public IActionResult StartingAuction([FromBody] StartAuction startAuction)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicleById(startAuction.VehicleId);
                if (vehicle == null)
                    return NotFound("Vehicle not found.");

                var auction = Mapper.VehicleToAuction(vehicle);

                _auctionService.CreateAuction(auction);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Auctions/PlaceBid")]
        public IActionResult PlaceBid([FromBody] PlaceBid placeBid)
        {
            try
            {
                _auctionService.PlaceBid(placeBid);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Auction/Close/{auctionId}")]
        public IActionResult CloseAuction(Guid auctionId)
        {
            try
            {
                var result = _auctionService.CloseAuctionById(auctionId);
                if (result == null)
                    return NotFound();

                _vehicleService.DeleteVehicle(result.VehicleId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Auctions/All")]
        public IActionResult GetAllAuctions()
        {
            try
            {
                var result = _auctionService.GetAllAuctions();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
