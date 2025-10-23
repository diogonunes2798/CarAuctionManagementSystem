namespace CarAuction.Models.Auctions
{
    public class Auction
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid VehicleId { get; init; }
        public double Bid { get; set; }
        public bool IsActive { get; set; } = true;

        public void CloseAuction()
        {
            IsActive = false;
        }
    }
}
