namespace CarAuction.Models.Vehicles
{
    public class Hatchback :Vehicle
    {
        public Hatchback()
        {
            Type = VehicleType.Hatchback;
        }
        public int NumberOfDoors { get; set; }
    }
}
