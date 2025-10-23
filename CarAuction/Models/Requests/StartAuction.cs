namespace CarAuction.Models.Requests
{
    public class StartAuction
    {
        public required Guid VehicleId { get; init; }
    }
}
