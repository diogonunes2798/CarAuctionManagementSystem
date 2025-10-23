namespace CarAuction.Models.Vehicles
{
    public class Truck : Vehicle
    {
        public Truck()
        {
            Type = VehicleType.Truck;
        }
        public double LoadCapacity { get; set; }
    }
}
