using CarAuction.Models;
using CarAuction.Models.Vehicles;

namespace CarAuction.Services
{
    public interface IVehicleService
    {
        void CreateVehicle(Vehicle vehicle);
        IReadOnlyList<Vehicle> GetAllVehicles();
        List<Vehicle> GetVehiclesByType(VehicleType type);
        List<Vehicle> GetVehiclesByModel(string model);
        List<Vehicle> GetVehiclesByManufacturer(string manufacturer);
        List<Vehicle> GetVehiclesByYear(int year);
        Vehicle? GetVehicleById(Guid vehicleId);
        void DeleteVehicle(Guid vehicleId);
    }
}
