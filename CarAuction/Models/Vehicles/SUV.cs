namespace CarAuction.Models.Vehicles
{
    public class SUV : Vehicle
    {
        public SUV()
        {
            Type = VehicleType.SUV;
        }
        public int NumberOfSeats { get; set; }
    }
}
