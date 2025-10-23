namespace CarAuction.Models.Requests
{
    public class PlaceBid
    {

        public required Guid VehicleId { get; init; }
        public required double BidAmount { get; set; }
    }
}
