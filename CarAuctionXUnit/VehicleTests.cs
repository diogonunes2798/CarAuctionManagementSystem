using CarAuction.Models;
using CarAuction.Models.Vehicles;
using CarAuction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarAuctionXUnit
{
    public class VehicleTests
    {
        private readonly IVehicleService _vehicleService;
        public VehicleTests()
        {
            _vehicleService = new VehicleService();
        }

        #region Create Vehicle
        [Fact]
        public void CreateVehicle_ShouldAddVehicle_WhenIdIsUnique()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Manufacturer = "Toyota",
                Model = "Corolla",
                Year = 2020,
                Type = VehicleType.Sudan,
                StartingBid = 10000
            };

            // Act
             _vehicleService.CreateVehicle(vehicle);

            // Assert
            Assert.Contains(vehicle, _vehicleService.GetAllVehicles());
        }

        [Fact]
        public void CreateVehicle_ShouldThrowException_WhenVehicleIdAlreadyExists()
        {
            var id = Guid.NewGuid();

            var vehicle1 = new Vehicle
            {
                Id = id,
                Manufacturer = "Ford",
                Model = "Focus",
                Year = 2018,
                Type = VehicleType.Hatchback,
                StartingBid = 10000
            };

            var vehicle2 = new Vehicle
            {
                Id = id,
                Manufacturer = "Ford",
                Model = "Fiesta",
                Year = 2019,
                Type = VehicleType.Hatchback,
                StartingBid = 10000
            };

            _vehicleService.CreateVehicle(vehicle1);

            var ex = Assert.Throws<InvalidOperationException>(() => _vehicleService.CreateVehicle(vehicle2));
            Assert.Equal($"Vehicle with ID {id} already exists.", ex.Message);
        }
        #endregion

        #region Search Vehicle
        [Fact]
        public void GetVehiclesByType_ShouldReturnMatchingVehicles()
        {
            var sudan = new Vehicle
            {
                Manufacturer = "Honda",
                Model = "Civic",
                Year = 2020,
                Type = VehicleType.Sudan,
                StartingBid = 10000
            };

            var suv = new Vehicle
            {
                Manufacturer = "Toyota",
                Model = "RAV4",
                Year = 2021,
                Type = VehicleType.SUV,
                StartingBid = 15000
            };

            _vehicleService.CreateVehicle(sudan);
            _vehicleService.CreateVehicle(suv);

            var result = _vehicleService.GetVehiclesByType(VehicleType.Sudan);

            Assert.True(result.Any());
            Assert.Contains(sudan, result);
        }

        [Fact]
        public void GetVehiclesByModel_ShouldReturnVehiclesCaseInsensitive()
        {
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Manufacturer = "Mazda",
                Model = "MX-5",
                Year = 2022,
                Type = VehicleType.Hatchback,
                StartingBid = 20000
            };

            _vehicleService.CreateVehicle(vehicle);

            var result = _vehicleService.GetVehiclesByModel("mx-5");

            Assert.True(result.Any());
            Assert.Equal(vehicle.Id, result.FirstOrDefault()?.Id);
        }

        [Fact]
        public void GetVehiclesByManufacturer_ShouldReturnVehiclesCaseInsensitive()
        {
            var vehicle = new Vehicle
            {
                Manufacturer = "Peugeot",
                Model = "208",
                Year = 2021,
                Type = VehicleType.Hatchback,
                StartingBid = 13000
            };

            _vehicleService.CreateVehicle(vehicle);

            var result = _vehicleService.GetVehiclesByManufacturer("peugeot");

            Assert.True(result.Any());
            Assert.Equal(vehicle.Id, result.FirstOrDefault()?.Id);
        }

        [Fact]
        public void GetVehiclesByYear_ShouldReturnCorrectVehicles()
        {
            var vehicle1 = new Vehicle
            {
                Manufacturer = "Renault",
                Model = "Clio",
                Year = 2020,
                Type = VehicleType.Hatchback,
                StartingBid = 11000
            };

            var vehicle2 = new Vehicle
            {
                Manufacturer = "Renault",
                Model = "Megane",
                Year = 2020,
                Type = VehicleType.Sudan,
                StartingBid = 14000
            };

            _vehicleService.CreateVehicle(vehicle1);
            _vehicleService.CreateVehicle(vehicle2);

            var result = _vehicleService.GetVehiclesByYear(2020);

            Assert.NotNull(result);
            Assert.Contains(vehicle1, result);
            Assert.Contains(vehicle2, result);
        }

        [Fact]
        public void GetVehicleById_ShouldReturnCorrectVehicle()
        {
            var vehicle = new Vehicle
            {
                Manufacturer = "BMW",
                Model = "320i",
                Year = 2019,
                Type = VehicleType.Sudan,
                StartingBid = 25000
            };

            _vehicleService.CreateVehicle(vehicle);

            var result = _vehicleService.GetVehicleById(vehicle.Id);

            Assert.NotNull(result);
            Assert.Equal(vehicle.Id, result.Id);
        }
        #endregion

        #region Delete Vehicle
        [Fact]
        public void DeleteVehicle_ShouldRemoveVehicle_WhenVehicleExists()
        {

            var vehicle = new Vehicle
            {
                Manufacturer = "Citroen",
                Model = "C4",
                Year = 2017,
                Type = VehicleType.Sudan,
                StartingBid = 9000
            };

            _vehicleService.CreateVehicle(vehicle);

            _vehicleService.DeleteVehicle(vehicle.Id);
            var stillExists = _vehicleService.GetVehicleById(vehicle.Id);

            Assert.Null(stillExists);
        }

        [Fact]
        public void DeleteVehicle_ShouldThrowException_WhenVehicleDoesNotExist()
        {
            var invalidId = Guid.NewGuid();

            var ex = Assert.Throws<InvalidOperationException>(() => _vehicleService.DeleteVehicle(invalidId));
            Assert.Equal($"Vehicle doesn't exists.", ex.Message);
        }
        #endregion
    }
}
