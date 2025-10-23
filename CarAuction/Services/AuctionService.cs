using CarAuction.Models;
using CarAuction.Models.Auctions;
using CarAuction.Models.Requests;
using CarAuction.Models.Vehicles;

namespace CarAuction.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly List<Auction> _auctions = new List<Auction>();

        public void CreateAuction(Auction auction)
        {
            if (_auctions.Any(x => x.VehicleId == auction.VehicleId && x.IsActive))
                throw new InvalidOperationException("An active auction already exists for this vehicle.");

            _auctions.Add(auction);
        }

        public void PlaceBid(PlaceBid placeBid)
        {
            var auction = GetAuctionByVehicleId(placeBid.VehicleId);

            if (auction == null)
                throw new InvalidOperationException("Auction not found.");

            BidValidator(auction, placeBid);

            auction.Bid = placeBid.BidAmount;
        }

        public Auction? GetAuctionById(Guid id)
        {
            return _auctions.FirstOrDefault(x => x.Id == id);
        }

        public Auction? GetAuctionByVehicleId(Guid vehicleId)
        {
            return _auctions.FirstOrDefault(x => x.VehicleId == vehicleId);
        }

        public IReadOnlyList<Auction> GetAllAuctions()
        {
            return _auctions;
        }

        public Auction? CloseAuctionById(Guid id)
        {
            var auction = GetAuctionById(id);

            if (auction == null || !auction.IsActive)
                return null;

            auction.CloseAuction();

            return auction;
        }

        private void BidValidator(Auction auction, PlaceBid bid)
        {
            if (!auction.IsActive)
                throw new InvalidOperationException("Cannot place a bid on an inactive auction.");

            if (bid.BidAmount <= 0)
                throw new InvalidOperationException("Bid amount must be greater than zero.");

            if (bid.BidAmount <= auction.Bid)
                throw new InvalidOperationException("Bid must be greater than current bid.");
        }
    }
}
