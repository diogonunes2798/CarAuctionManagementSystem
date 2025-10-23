namespace CarAuction.Models.Vehicles
{
    public class Sudan : Vehicle
    {
        public Sudan()
        {
            Type = VehicleType.Sudan;
        }
        public int NumberOfDoors { get; set; }
    }
}
