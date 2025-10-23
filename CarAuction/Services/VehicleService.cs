using CarAuction.Models;
using CarAuction.Models.Vehicles;

namespace CarAuction.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly List<Vehicle> _vehicles = new List<Vehicle>();

        public void CreateVehicle(Vehicle vehicle)
        {
            VehicleValidator(vehicle);

            if (_vehicles.Any(v => v.Id == vehicle.Id))
                throw new InvalidOperationException($"Vehicle with ID {vehicle.Id} already exists.");

            _vehicles.Add(vehicle);
        }

        public IReadOnlyList<Vehicle> GetAllVehicles()
        {
            return _vehicles;
        }

        public List<Vehicle> GetVehiclesByType(VehicleType type)
        {
            return _vehicles.Where(x => x.Type == type).ToList();
        }

        public List<Vehicle> GetVehiclesByModel(string model)
        {
            return _vehicles.Where(x => x.Model.ToLower() == model.ToLower()).ToList();
        }

        public List<Vehicle> GetVehiclesByManufacturer(string manufacturer)
        {
            return _vehicles.Where(x => x.Manufacturer.ToLower() == manufacturer.ToLower()).ToList();
        }

        public List<Vehicle> GetVehiclesByYear(int year)
        {
            return _vehicles.Where(x => x.Year == year).ToList();
        }

        public Vehicle? GetVehicleById(Guid vehicleId)
        {
            return _vehicles.FirstOrDefault(x => x.Id == vehicleId);
        }

        public void DeleteVehicle(Guid vehicleId)
        {
            var vehicle = _vehicles.FirstOrDefault(x => x.Id == vehicleId);

            if (vehicle is null)
                throw new InvalidOperationException($"Vehicle doesn't exists.");

            _vehicles.Remove(vehicle);
        }

        private void VehicleValidator(Vehicle vehicle)
        {

            if (vehicle.Id == Guid.Empty)
                throw new InvalidOperationException("Vehicle must have a valid ID.");

            if (string.IsNullOrWhiteSpace(vehicle.Manufacturer))
                throw new InvalidOperationException("Manufacturer is required.");

            if (string.IsNullOrWhiteSpace(vehicle.Model))
                throw new InvalidOperationException("Model is required.");

            if (vehicle.Year < 1900 || vehicle.Year > DateTime.UtcNow.Year)
                throw new InvalidOperationException("Year is out of valid range.");

            if (vehicle.StartingBid <= 0)
                throw new InvalidOperationException("Starting bid must be greater than zero.");
        }
    }
}
