namespace CarAuction.Models.Requests
{
    public class CreateVehicle
    {
        public required VehicleType Type { get; set; }

        public required string Manufacturer { get; set; } = string.Empty;

        public required string Model { get; set; } = string.Empty;

        public required int Year { get; set; }

        public required double StartingBid { get; set; }

        public required int CustomProperty { get; set; }
    }
}
