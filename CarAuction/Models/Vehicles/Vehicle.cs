namespace CarAuction.Models.Vehicles
{
    public class Vehicle
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public  VehicleType Type { get; set; }

        public required string Manufacturer { get; set; } = string.Empty;

        public required string Model { get; set; } = string.Empty;

        public required int Year { get; set; }

        public required double StartingBid { get; set; }
    }
}
