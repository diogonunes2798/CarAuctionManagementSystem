using CarAuction.Models.Auctions;
using CarAuction.Models.Requests;
using CarAuction.Models.Vehicles;

namespace CarAuction.Services
{
    public interface IAuctionService
    {
        void CreateAuction(Auction auction);
        void PlaceBid(PlaceBid placeBid);
        IReadOnlyList<Auction> GetAllAuctions();
        Auction? CloseAuctionById(Guid id);
        Auction? GetAuctionByVehicleId(Guid vehicleId);
    }
}
